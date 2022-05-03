using FoodSharing.Models.Users;
using FoodSharing.Services.Users.Converters;
using FoodSharing.Services.Users.Interfaces;
using FoodSharing.Tools.Database;
using Npgsql;

namespace FoodSharing.Services.Users.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DbConnection _dbConnection;

        public UserRepository(IConfiguration config)
        {
            _dbConnection = new DbConnection(config.GetConnectionString("DefaultConnection"));
        }

        public Task<User> GetUserByEmailAndPassword(string email, string password)
        {
            string expression = @"select * from usertest where email = @email and password = @password";

            NpgsqlParameter[] parameters = new[]
            {
                new NpgsqlParameter(nameof(email), email),
                new NpgsqlParameter(nameof(password), password),
            };

            return _dbConnection.Get<User>(expression, UserConverter.MapToUser, parameters);
        }
    }
}
