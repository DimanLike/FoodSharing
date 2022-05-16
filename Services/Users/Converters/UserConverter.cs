using FoodSharing.Models;
using FoodSharing.Models.Products;
using FoodSharing.Models.Users;
using Npgsql;

namespace FoodSharing.Services.Users.Converters
{
    public static class UserConverter
    {
        public static async Task<User> MapToUser(NpgsqlDataReader reader)
        {
            User user = new User();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    user.Id = (Guid)reader["ID"];
                    user.Email = (string)reader["Email"];
                    user.Password = (string)reader["Password"];
                    user.CreatedAt = (DateTime)reader["CreatedAt"];
                }
            }
            else
            {
                return null;
            }

            return user;
        }
        public static async Task<UserProfile> MapToUserProfile(NpgsqlDataReader reader)
        {
            UserProfile userProfile = new UserProfile();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    userProfile.Id = (Guid)reader["ID"];
                    userProfile.FirstName = reader["FirstName"] is DBNull ? null : (string)reader["FirstName"];
                    userProfile.LastName = reader["LastName"] is DBNull ? null : (string)reader["LastName"];
                    userProfile.Email = (string)reader["Email"];
                    userProfile.Adress = reader["Adress"] is DBNull ? null : (string)reader["Adress"];
                    userProfile.Phone = reader["Phone"] is DBNull ? null : (string)reader["Phone"];
                    userProfile.Avatar = reader["Avatar"] is DBNull ? null : (byte[])reader["Avatar"];
                }
            }
            else
            {
                return null;
            }
            return userProfile;
        }

        public static async Task<UserProducts> MapToUserProduct(NpgsqlDataReader reader)
		{
            UserProducts userProducts = new UserProducts();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    userProducts.Id = (Guid)reader["Id"];
                    userProducts.UserId = (Guid)reader["UserId"];
                    userProducts.Name = (string)reader["Name"];
                    userProducts.Description = (string)reader["Description"];
                    userProducts.Category = (string)reader["Category"];
                    userProducts.Quantity = (string)reader["Quantity"];
                    userProducts.Image = (byte[])reader["Image"];
                    userProducts.CreatedAt = (DateTime)reader["CreatedAt"];
                }
            }
            else
            {
                return null;
            }

            return userProducts;
        }

        public static async Task<List<UserProducts>> MapToUserProducts(NpgsqlDataReader reader)
        {
            List<UserProducts> userProducts = new List<UserProducts>();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    UserProducts userProduct = new UserProducts();
                    
                    userProduct.Id = (Guid)reader["Id"];
                    userProduct.UserId = (Guid)reader["UserId"];
                    userProduct.Name = (string)reader["Name"];
                    userProduct.Description = (string)reader["Description"];
                    userProduct.Category = (string)reader["Category"];
                    userProduct.Quantity = (string)reader["Quantity"];
                    userProduct.Image = (byte[])reader["Image"];
                    userProduct.CreatedAt = (DateTime)reader["CreatedAt"];

                    userProducts.Add(userProduct);
                }
            }
            else
            {
                return new List<UserProducts>();
            }

            return userProducts;
        }

        public static ProductsViewModel MapToUserProductsView(UserProducts userProducts)
		{
            return new ProductsViewModel(userProducts.Id,
                                         userProducts.UserId,
                                         userProducts.Name,
                                         userProducts.Description,
                                         userProducts.Category,
                                         userProducts.Quantity,
                                         userProducts.Image,
                                         userProducts.CreatedAt);
        }

        public static UserProfileViewModel MapToUserProfileView(UserProfile userProfile)
        {
            return new UserProfileViewModel(userProfile.Id, 
                                            userProfile.FirstName, 
                                            userProfile.LastName, 
                                            userProfile.Email, 
                                            userProfile.Adress, 
                                            userProfile.Phone, 
                                            userProfile.Avatar);
        }
    }    
}