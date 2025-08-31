using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace naidisprojekt.Controls
{
    public class SvgDrawable : IDrawable
    {
        public string SvgSource { get; set; }
        public Color FillColor { get; set; } = Colors.Black;
        public Thickness Padding { get; set; }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            if (string.IsNullOrEmpty(SvgSource)) return;

            try
            {
                var viewBox = ExtractViewBox(SvgSource);
                var pathData = ExtractPathData(SvgSource);

                if (string.IsNullOrEmpty(pathData)) return;

                var drawRect = new RectF(
                    dirtyRect.X + (float)Padding.Left,
                    dirtyRect.Y + (float)Padding.Top,
                    dirtyRect.Width - (float)(Padding.Left + Padding.Right),
                    dirtyRect.Height - (float)(Padding.Top + Padding.Bottom)
                );

                var path = ParseSvgPath(pathData);

                canvas.SaveState();
                canvas.FillColor = FillColor;
                ApplyTransform(canvas, viewBox, drawRect);
                canvas.FillPath(path);
                canvas.RestoreState();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error drawing SVG: {ex.Message}");
            }
        }

        private void ApplyTransform(ICanvas canvas, RectF viewBox, RectF bounds)
        {
            var scaleX = bounds.Width / viewBox.Width;
            var scaleY = bounds.Height / viewBox.Height;
            var scale = Math.Min(scaleX, scaleY);

            var scaledWidth = viewBox.Width * scale;
            var scaledHeight = viewBox.Height * scale;
            var offsetX = bounds.X + (bounds.Width - scaledWidth) / 2f - viewBox.X * scale;
            var offsetY = bounds.Y + (bounds.Height - scaledHeight) / 2f - viewBox.Y * scale;

            canvas.Translate(offsetX, offsetY);
            canvas.Scale(scale, scale);
        }

        private PathF ParseSvgPath(string pathData)
        {
            var path = new PathF();
            var commands = Regex.Split(pathData, @"(?=[MmLlHhVvCcSsQqTtAaZz])").Where(c => !string.IsNullOrWhiteSpace(c));

            float currentX = 0, currentY = 0;
            float controlX = 0, controlY = 0;
            char lastCmd = ' ';

            foreach (var command in commands)
            {
                if (string.IsNullOrWhiteSpace(command)) continue;

                var cmd = command[0];
                var values = ExtractNumbers(command.Substring(1));
                int i = 0;

                while (i < values.Count)
                {
                    switch (cmd)
                    {
                        case 'M': currentX = values[i++]; currentY = values[i++]; path.MoveTo(currentX, currentY); break;
                        case 'm': currentX += values[i++]; currentY += values[i++]; path.MoveTo(currentX, currentY); break;
                        case 'L': currentX = values[i++]; currentY = values[i++]; path.LineTo(currentX, currentY); break;
                        case 'l': currentX += values[i++]; currentY += values[i++]; path.LineTo(currentX, currentY); break;
                        case 'H': currentX = values[i++]; path.LineTo(currentX, currentY); break;
                        case 'h': currentX += values[i++]; path.LineTo(currentX, currentY); break;
                        case 'V': currentY = values[i++]; path.LineTo(currentX, currentY); break;
                        case 'v': currentY += values[i++]; path.LineTo(currentX, currentY); break;
                        case 'C': controlX = values[i + 2]; controlY = values[i + 3]; currentX = values[i + 4]; currentY = values[i + 5]; path.CurveTo(values[i], values[i + 1], controlX, controlY, currentX, currentY); i += 6; break;
                        case 'c': path.CurveTo(currentX + values[i], currentY + values[i + 1], currentX + values[i + 2], currentY + values[i + 3], currentX + values[i + 4], currentY + values[i + 5]); controlX = currentX + values[i + 2]; controlY = currentY + values[i + 3]; currentX += values[i + 4]; currentY += values[i + 5]; i += 6; break;
                        case 'S':
                            float reflectX = 2 * currentX - controlX;
                            float reflectY = 2 * currentY - controlY;
                            if (lastCmd != 'c' && lastCmd != 'C' && lastCmd != 's' && lastCmd != 'S') { reflectX = currentX; reflectY = currentY; }
                            path.CurveTo(reflectX, reflectY, values[i], values[i + 1], values[i + 2], values[i + 3]); controlX = values[i]; controlY = values[i + 1]; currentX = values[i + 2]; currentY = values[i + 3]; i += 4;
                            break;
                        case 's':
                            float reflectRelX = 2 * currentX - controlX;
                            float reflectRelY = 2 * currentY - controlY;
                            if (lastCmd != 'c' && lastCmd != 'C' && lastCmd != 's' && lastCmd != 'S') { reflectRelX = currentX; reflectRelY = currentY; }
                            path.CurveTo(reflectRelX, reflectRelY, currentX + values[i], currentY + values[i + 1], currentX + values[i + 2], currentY + values[i + 3]); controlX = currentX + values[i]; controlY = currentY + values[i + 1]; currentX += values[i + 2]; currentY += values[i + 3]; i += 4;
                            break;
                        case 'Q': controlX = values[i]; controlY = values[i + 1]; currentX = values[i + 2]; currentY = values[i + 3]; path.QuadTo(controlX, controlY, currentX, currentY); i += 4; break;
                        case 'q': path.QuadTo(currentX + values[i], currentY + values[i + 1], currentX + values[i + 2], currentY + values[i + 3]); controlX = currentX + values[i]; controlY = currentY + values[i + 1]; currentX += values[i + 2]; currentY += values[i + 3]; i += 4; break;
                        case 'Z': case 'z': path.Close(); break;
                        default: i++; break;
                    }
                    if (char.IsLetter(cmd) && char.IsUpper(cmd)) { lastCmd = cmd; } else if (char.IsLetter(cmd)) { lastCmd = char.ToUpper(cmd); }
                }
            }
            return path;
        }

        private List<float> ExtractNumbers(string input)
        {
            var numbers = new List<float>();
            var matches = Regex.Matches(input, @"-?[\d\.]+([eE]-?\d+)?");
            foreach (Match match in matches)
            {
                if (float.TryParse(match.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out float value))
                {
                    numbers.Add(value);
                }
            }
            return numbers;
        }

        private RectF ExtractViewBox(string svgString)
        {
            var match = Regex.Match(svgString, @"<svg[^>]+viewBox\s*=\s*[""']([^""']+)[""']", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                var parts = match.Groups[1].Value.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 4)
                {
                    return new RectF(
                        float.Parse(parts[0], CultureInfo.InvariantCulture),
                        float.Parse(parts[1], CultureInfo.InvariantCulture),
                        float.Parse(parts[2], CultureInfo.InvariantCulture),
                        float.Parse(parts[3], CultureInfo.InvariantCulture)
                    );
                }
            }
            return new RectF(0, 0, 24, 24);
        }

        private string ExtractPathData(string svgString)
        {
            var match = Regex.Match(svgString, @"<path[^>]+d\s*=\s*[""']([^""']+)[""']", RegexOptions.IgnoreCase);
            return match.Success ? match.Groups[1].Value : string.Empty;
        }
    }
}
