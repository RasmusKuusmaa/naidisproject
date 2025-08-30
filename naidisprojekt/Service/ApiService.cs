
using naidisprojekt.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace naidisprojekt.Service
{
    public class ApiService
    {
        private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("http://10.0.2.2:5999/api/"),
        Timeout = TimeSpan.FromSeconds(5)};
       

        public async Task<bool> RegisterAnUser(string username, string email, string password)
        {
            try
            {

            var requestData = new
            {
                Name = username,
                Email = email,
                password = password
            };
            var content = new StringContent(
                JsonSerializer.Serialize(requestData),
                Encoding.UTF8,
                "application/json");
            var response = await _httpClient.PostAsync("user/register", content);
            return response.IsSuccessStatusCode;
            } catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> Login(string email, string password)
        {
            try
            {
                var requestdata = new 
                {
                    Email = email,
                    Password = password
                };
                var content = new StringContent(JsonSerializer.Serialize(requestdata), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("user/login", content);
                if(response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var user = JsonSerializer.Deserialize<User>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    if (user != null) 
                        UserSession.CurrentUser = user;
                }
                return response.IsSuccessStatusCode;
            } catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
