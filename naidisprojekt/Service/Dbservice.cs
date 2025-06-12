using MySql.Data.MySqlClient;
using naidisprojekt.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

    }
  }
