using MySql.Data.MySqlClient;
using naidisprojekt.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace naidisprojekt.Service
{
    public class Dbservice
    {
        private string connectionString = "server=192.168.1.172;user=root;password=qwerty;database=naidisprojekt;port=3306;";

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var products = new List<Product>();

            using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            string query = "SELECT Id, Name, Description, Price, CategoryId, ImageSource FROM Products";

            using var cmd = new MySqlCommand(query, connection);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                products.Add(new Product
                {
                    Id = reader.GetInt32("Id"),
                    Name = reader.GetString("Name"),
                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? null : reader.GetString("Description"),
                    Price = reader.GetInt32("Price"),
                    CategoryId = reader.GetInt32("CategoryId"),
                    ImageSource = reader.IsDBNull(reader.GetOrdinal("ImageSource")) ? null : reader.GetString("ImageSource")
                });
            }

            return products;
        }

        public async Task<int?> GetUserIdAsync(string username, string password)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string hashedPassword = HashPassword(password);

                string query = "SELECT id FROM users WHERE email = @username AND Pass = @password";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", hashedPassword);

                    var result = await command.ExecuteScalarAsync();
                    return result != null ? Convert.ToInt32(result) : (int?)null;
                }
            }
        }

        public async Task RegisterUserAsync(string userName, string email, string password)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = "INSERT INTO users (username, email, Pass) VALUES (@username, @email, @password);";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        string hashedPassword = HashPassword(password);

                        command.Parameters.AddWithValue("@username", userName);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@password", hashedPassword);

                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        Console.WriteLine($"Rows affected: {rowsAffected}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT id, username FROM users WHERE id = @id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", userId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new User
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            };
                        }
                    }
                }
            }

            return null;
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}
