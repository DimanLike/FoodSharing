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

		public Task AddUser(string email, string password)
		{
			string expression = @"INSERT INTO users (id, email, password, createdat) 
								VALUES (@id, @email, @password, @createdat)";
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

		public Task<User> GetUserByEmail(string email)
		{
			string expression = @"SELECT * FROM users WHERE email = @email";

			NpgsqlParameter[] parameters = new[]
			{
				new NpgsqlParameter(nameof(email), email),
			};
			return _dbConnection.Get(expression, UserConverter.MapToUser, parameters);
		}

		public Task<User> GetUserById(Guid userid)
		{
			string expression = @"SELECT * FROM users WHERE id = @userid";

			NpgsqlParameter[] parameters = new[]
			{
				new NpgsqlParameter(nameof(userid), userid),
			};
			return _dbConnection.Get(expression, UserConverter.MapToUser, parameters);
		}

		public Task AddUserProfile(UserProfileView model)
        {
			string expression = @"INSERT INTO usersprofile(id, userid, firstname, lastname, email, adress, phone, avatar)
				VALUES(@id, @userid, @firstname, @lastname, @email, @adress, @phone, @avatar)
				ON CONFLICT (id) DO UPDATE SET
					firstname = EXCLUDED.firstname,
					lastname = EXCLUDED.lastname,
					adress = EXCLUDED.adress,
					phone = EXCLUDED.phone, 
					avatar = EXCLUDED.avatar;";

			NpgsqlParameter[] parameters = new[]
			{
				new NpgsqlParameter(nameof(model.Id), model.Id),
				new NpgsqlParameter(nameof(model.UserId), model.UserId),
				new NpgsqlParameter(nameof(model.FirstName), model.FirstName),
				new NpgsqlParameter(nameof(model.LastName), model.LastName),
				new NpgsqlParameter(nameof(model.Email), model.Email),
				new NpgsqlParameter(nameof(model.Adress), model.Adress),
				new NpgsqlParameter(nameof(model.Phone), model.Phone),
				new NpgsqlParameter(nameof(model.Avatar), model.Avatar),
			};

			return _dbConnection.Add(expression, parameters);
		}

		public Task<UserProfile> GetUserProfile(Guid userid)
        {
			string expression = @"SELECT * FROM usersprofile WHERE userid = @userid";

			NpgsqlParameter[] parameters = new[]
			{
				new NpgsqlParameter(nameof(userid), userid),
			};

			return _dbConnection.Get(expression, UserConverter.MapToUserProfile, parameters);
		}

		public Task<List<User>> GetUsers(Guid[] ids)
		{
			string expression = @"SELECT * FROM users WHERE id = ANY(@ids)";

			NpgsqlParameter[] parameters = new[]
			{
				new NpgsqlParameter(nameof(ids), ids),
			};

			return _dbConnection.GetList(expression, UserConverter.MapToUsers, parameters);
		}
	}
}
