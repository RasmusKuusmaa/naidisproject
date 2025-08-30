using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using NaidisprojektAPi.Helpers;
using NaidisprojektAPi.Models;
using System.Data;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace NaidisprojektAPi.Repository
{
    public class UserRepository
    {
        private string connectionString;
        public UserRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<ValidateUserRespoonse?> Validateuser(string email, string password)
        {
            try
            {
                await using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = @"SELECT [user_id], user_name from Users where [email] = @email and [password] = @password;";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", passwordHelper.HashPassword(password));
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new ValidateUserRespoonse
                            {
                                Email = email,
                                UserId = reader.GetInt32(0),
                                UserName = reader.GetString(1)
                            };
                        }
                    }
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> RegisterUser(string name, string password, string email)
        {
            try
            {
                await using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = @"INSERT INTO Users ([user_name], [password], [email]) values (@name, @password, @email);";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@password", passwordHelper.HashPassword(password));
                    cmd.Parameters.AddWithValue("@email", email);
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<ListingResponse>> GetAllListings()
        {
            List<ListingResponse> Listings = new List<ListingResponse>();
            try
            {
                await using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = @"SELECT [listing_id],[price],[listing_name],[listing_description],[listing_image],[category_id],[user_id] FROM [naidisprojekt].[dbo].[Listings]";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Listings.Add(new ListingResponse
                            {
                                ListingId = reader.GetInt32(0),
                                Price = reader.GetDecimal(1),
                                ListingName = reader.GetString(2),
                                ListingDescription = reader.GetString(3),
                                ListingImage = reader.IsDBNull(4) ? null : reader.GetFieldValue<byte[]>(4),
                                CategoryId = reader.GetInt32(5),
                                UserId = reader.GetInt32(6),
                            });
                        }
                    }
                }
            }
            catch
            {
            }
            return Listings;
        }

        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            List<CategoryResponse> Categories = new List<CategoryResponse>();
            try
            {
                await using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = @"SELECT [category_id],[category_name],[category_image] FROM [naidisprojekt].[dbo].[Categories]";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Categories.Add(new CategoryResponse
                            {
                                CategoryId = reader.GetInt32(0),
                                CategoryName = reader.GetString(1),
                                CategoryImage = reader.IsDBNull(2) ? null : reader.GetFieldValue<byte[]>(2)
                            });
                        }
                    }
                }
            }
            catch
            {
            }
            return Categories;
        }

        public async Task<bool> Addnewlisting(ListingResponse request)
        {
            try
            {
                await using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = @"INSERT INTO Listings ([price],[listing_name],[listing_description],[listing_image],[category_id],[user_id]) values (@price, @listing_name, @listing_description, @listing_image, @category_id, @user_id);";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@price", request.Price);
                    cmd.Parameters.AddWithValue("@listing_name", request.ListingName);
                    cmd.Parameters.AddWithValue("@listing_description", request.ListingDescription);
                    cmd.Parameters.Add("@listing_image", SqlDbType.VarBinary, -1).Value = request.ListingImage ?? (object)DBNull.Value;
                    cmd.Parameters.AddWithValue("@category_id", request.CategoryId);
                    cmd.Parameters.AddWithValue("@user_id", request.UserId);
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<List<ListingResponse>> GetListingsByUser(int userid)
        {
            List<ListingResponse> Listings = new List<ListingResponse>();
            try
            {
                await using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = @"SELECT [listing_id],[price],[listing_name],[listing_description],[listing_image],[category_id],[user_id] FROM [naidisprojekt].[dbo].[Listings] where [user_id] = @userId";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@userId", userid);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Listings.Add(new ListingResponse
                            {
                                ListingId = reader.GetInt32(0),
                                Price = reader.GetDecimal(1),
                                ListingName = reader.GetString(2),
                                ListingDescription = reader.GetString(3),
                                ListingImage = reader.IsDBNull(4) ? null : reader.GetFieldValue<byte[]>(4),
                                CategoryId = reader.GetInt32(5),
                                UserId = reader.GetInt32(6),
                            });
                        }
                    }
                }
            }
            catch
            {
            }
            return Listings;
        }

        public async Task<List<ListingResponse>> GetFavoriteListings(int userid)
        {
            List<ListingResponse> Listings = new List<ListingResponse>();
            try
            {
                await using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = @"SELECT l.[listing_id],l.[price],l.[listing_name],l.[listing_description],l.[listing_image],l.[category_id],l.[user_id] FROM [naidisprojekt].[dbo].[UserFavorites] uf INNER JOIN [naidisprojekt].[dbo].[Listings] l on uf.listing_id = l.listing_id where uf.[user_id] = @userId";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@userId", userid);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Listings.Add(new ListingResponse
                            {
                                ListingId = reader.GetInt32(0),
                                Price = reader.GetDecimal(1),
                                ListingName = reader.GetString(2),
                                ListingDescription = reader.GetString(3),
                                ListingImage = reader.IsDBNull(4) ? null : reader.GetFieldValue<byte[]>(4),
                                CategoryId = reader.GetInt32(5),
                                UserId = reader.GetInt32(6),
                            });
                        }
                    }
                }
            }
            catch
            {
            }
            return Listings;
        }

        public async Task<bool> RemoveListingFromFavorites(int UserFavoriteId)
        {
            try
            {
                await using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = @"DELETE FROM [naidisprojekt].[dbo].[UserFavorites] WHERE user_favorite_id = @id";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", UserFavoriteId);
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteListing(int listingId)
        {
            try
            {
                await RemoveAllFavoritesForListing(listingId);
                await using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = @"DELETE FROM [naidisprojekt].[dbo].[Listings] WHERE [listing_id] = @listingId";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@listingId", listingId);
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
        public async Task<bool> RemoveAllFavoritesForListing(int listingId)
        {
            try
            {
                await using SqlConnection connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                string query = @"DELETE FROM UserFavorites WHERE listing_id = @listingId";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@listingId", listingId);
                return await cmd.ExecuteNonQueryAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateUserInformation(int id, string name, string email)
        {
            try
            {
                await using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = @"UPDATE Users set [user_name] = @name, [email] = @email where [user_id] = @id";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@email", email);
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> FavoriteAnListing(int userId, int listingId)
        {
            try
            {
                await using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = @"INSERT INTO UserFavorites ([listing_id]
                    ,[user_id]) values (@listingId, @userId);";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@listingId", listingId);
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            } catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
