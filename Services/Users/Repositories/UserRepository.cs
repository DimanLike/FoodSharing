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

		public Task AddUserByEmailAndPassword(string email, string password)
		{
			string expression = @"INSERT INTO usertest(id, email, password) VALUES(@id, @email, @password)";
			Guid id = Guid.NewGuid();

			NpgsqlParameter[] parameters = new[]
		    {
				new NpgsqlParameter(nameof(id), id),
				new NpgsqlParameter(nameof(email), email),
				new NpgsqlParameter(nameof(password), password),
			};

			return _dbConnection.Add(expression, parameters);
		}
	}
}
