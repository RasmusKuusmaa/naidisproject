using naidisprojekt.Models;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;

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

        public async Task<bool> AddnewListing(Listing listing)
        {
            Debug.WriteLine("try to add new listing");
            try
            {
                var requestData = new
                {
                    Price = listing.Price,
                    ListingName = listing.ListingName,
                    ListingDescription = listing.ListingDescription,
                    CategoryId = listing.CategoryId,
                    ListingImage = listing.ListingImage,
                    UserId = UserSession.CurrentUser.UserId
                };
                var content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("user/listings/new", content);
                Debug.WriteLine("new listing false");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("failed to add new listing");

                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<List<Category>> GetAllCategories()
        {
            Debug.WriteLine("started getting all categories");
            try
            {
                var response = await _httpClient.GetAsync("user/categories");
                if (!response.IsSuccessStatusCode)
                    return new List<Category>();
                var json = await response.Content.ReadAsStringAsync();
                var categories = JsonSerializer.Deserialize<List<Category>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                Debug.WriteLine(categories[0].CategoryName);
                return categories ?? new List<Category>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new List<Category>();
            }
        } 

        public async Task<List<Listing>> GetAllListings()
        {
            try
            {
                var response = await _httpClient.GetAsync("user/listings");
                if (!response.IsSuccessStatusCode)
                    return new List<Listing>();
                var json = await response.Content.ReadAsStringAsync();
                var categories = JsonSerializer.Deserialize<List<Listing>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return categories ?? new List<Listing>();
            } catch (Exception ex)
            {
                return new List<Listing>();
            }
        }

        public async Task<bool> AddListingToFavorites(int userId, int listingId)
        {
            try
            {

                var request = new
                {
                    UserId = userId,
                    ListingId = listingId
                };
                var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("user/listings/favorites/add", content);
                return response.IsSuccessStatusCode;
            } catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
        
        public async Task<List<Listing>> GetUserFavoriteListings(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"user/listings/favorites/{userId}");
                if (!response.IsSuccessStatusCode)
                    return new List<Listing>();
                var json = await response.Content.ReadAsStringAsync();
                var categories = JsonSerializer.Deserialize<List<Listing>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return categories ?? new List<Listing>();
            }
            catch (Exception ex)
            {
                return new List<Listing>();
            }
        }

        public async Task<List<Listing>> GetUserListings(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"user/listings/{userId}");
                if (!response.IsSuccessStatusCode)
                    return new List<Listing>();
                var json = await response.Content.ReadAsStringAsync();
                var categories = JsonSerializer.Deserialize<List<Listing>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return categories ?? new List<Listing>();
            }
            catch (Exception ex)
            {
                return new List<Listing>();
            }
        }
    }
}
