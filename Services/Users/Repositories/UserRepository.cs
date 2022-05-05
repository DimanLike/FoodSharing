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

			return _dbConnection.Get<User>(expression, UserConverter.MapToUser, parameters);
		}

		public Task AddUserByEmailAndPassword(string email, string password)
		{
			string expression = @"INSERT INTO usertest(id, email, password, datecreate ) VALUES(@id, @email, @password, @datecreate)";
			Guid id = Guid.NewGuid();
			DateTime datecreate = DateTime.Now; 


			NpgsqlParameter[] parameters = new[]
		    {
				new NpgsqlParameter(nameof(id), id),
				new NpgsqlParameter(nameof(email), email),
				new NpgsqlParameter(nameof(password), password),
				new NpgsqlParameter(nameof(datecreate), datecreate),
			};

			return _dbConnection.Add(expression, parameters);
		}

		public Task AddUserDataProfile(Guid id, string firstname, string lastname, string email, string adress, string phone)
        {
			string expression = @"INSERT INTO profiletest(id, firstname, lastname, email, adress,phone) VALUES(@id, @firstname, @lastname, @email, @adress, @phone)";

			NpgsqlParameter[] parameters = new[]
			{
				new NpgsqlParameter(nameof(id), id),
				new NpgsqlParameter(nameof(firstname), firstname),
				new NpgsqlParameter(nameof(lastname), lastname),
				new NpgsqlParameter(nameof(email), email),
				new NpgsqlParameter(nameof(adress),adress),
				new NpgsqlParameter(nameof(phone),phone),
			};
			return _dbConnection.Add(expression, parameters);
		}
	}
}
