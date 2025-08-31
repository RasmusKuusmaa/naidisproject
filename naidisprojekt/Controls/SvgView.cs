using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace naidisprojekt.Controls
{
    public class SvgView : GraphicsView
    {
        public static readonly BindableProperty SvgSourceProperty = BindableProperty.Create(
            nameof(SvgSource), typeof(string), typeof(SvgView), string.Empty, propertyChanged: OnSvgSourceChanged);

        public static readonly BindableProperty FillColorProperty = BindableProperty.Create(
            nameof(FillColor), typeof(Color), typeof(SvgView), Colors.Black, propertyChanged: OnFillColorChanged);

        public static readonly BindableProperty PaddingProperty = BindableProperty.Create(
            nameof(Padding), typeof(Thickness), typeof(SvgView), new Thickness(0), propertyChanged: OnPaddingChanged);

        public string SvgSource
        {
            get => (string)GetValue(SvgSourceProperty);
            set => SetValue(SvgSourceProperty, value);
        }

        public Color FillColor
        {
            get => (Color)GetValue(FillColorProperty);
            set => SetValue(FillColorProperty, value);
        }

        public Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }

        public SvgView()
        {
            Drawable = new SvgDrawable
            {
                Padding = this.Padding
            };
        }

        private static void OnSvgSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is SvgView svgView && svgView.Drawable is SvgDrawable drawable)
            {
                drawable.SvgSource = newValue as string;
                drawable.FillColor = svgView.FillColor;
                drawable.Padding = svgView.Padding;
                svgView.Invalidate();
            }
        }

        private static void OnFillColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is SvgView svgView && svgView.Drawable is SvgDrawable drawable)
            {
                drawable.FillColor = (Color)newValue;
                svgView.Invalidate();
            }
        }

        private static void OnPaddingChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is SvgView svgView && svgView.Drawable is SvgDrawable drawable)
            {
                drawable.Padding = (Thickness)newValue;
                svgView.Invalidate();
            }
        }
    }
}