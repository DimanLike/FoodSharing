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
                }
            }
            else
            {
                return null;
            }

            return user;
        }
    }
}
