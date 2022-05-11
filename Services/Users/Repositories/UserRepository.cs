using FoodSharing.Models;
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

		public Task<User> GetUserByEmailAndPassword(string email)
		{
			string expression = @"SELECT * FROM usertest WHERE email = @email";

			NpgsqlParameter[] parameters = new[]
			{
				new NpgsqlParameter(nameof(email), email),
			};
			return _dbConnection.Get(expression, UserConverter.MapToUser, parameters);
		}

		public Task AddUserByEmailAndPassword(string email, string password)
		{
			string expression = @"INSERT INTO usertest (id, email, password, createdat) VALUES (@id, @email, @password, @createdat)";
			Guid id = Guid.NewGuid();
			DateTime createdAt = DateTime.Now; 

			NpgsqlParameter[] parameters = new[]
		    {
				new NpgsqlParameter(nameof(id), id),
				new NpgsqlParameter(nameof(email), email),
				new NpgsqlParameter(nameof(password), password),
				new NpgsqlParameter(nameof(createdAt), createdAt),
			};

			return _dbConnection.Add(expression, parameters);
		}

		public Task AddUserDataProfile(UserProfileViewModel model)
        {
			string expression = @"INSERT INTO profiletest(id, firstname, lastname, email, adress,phone) VALUES(@id, @firstname, @lastname, @email, @adress, @phone) ON CONFLICT (id) DO UPDATE SET firstname = EXCLUDED.firstname, lastname = EXCLUDED.lastname, adress = EXCLUDED.adress, phone = EXCLUDED.phone ; ";

			var user = model.GetType;



			NpgsqlParameter[] parameters = new[]
			{
				new NpgsqlParameter(nameof(model.Id), model.Id),
				new NpgsqlParameter(nameof(model.FirstName), model.FirstName),
				new NpgsqlParameter(nameof(model.LastName), model.LastName),
				new NpgsqlParameter(nameof(model.Email), model.Email),
				new NpgsqlParameter(nameof(model.Adress), model.Adress),
				new NpgsqlParameter(nameof(model.Phone), model.Phone),
			};
			return _dbConnection.Add(expression, parameters);
		}

		public Task<UserProfile> GetUserDataProfile(string email)
        {
			string expression = @"SELECT * FROM profiletest WHERE email = @email";

			NpgsqlParameter[] parameters = new[]
			{
				new NpgsqlParameter(nameof(email), email),
			};

			return _dbConnection.Get(expression, UserConverter.MapToUserProfile, parameters);
		}
	}
}
