using FoodSharing.Models.Chat;
using FoodSharing.Services.Chat.Converters;
using FoodSharing.Services.Chat.Interfaces;
using FoodSharing.Tools.Database;
using Npgsql;

namespace FoodSharing.Services.Chat.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private DbConnection _dbConnection;

        public ChatRepository(IConfiguration config)
        {
            _dbConnection = new DbConnection(config.GetConnectionString("DefaultConnection"));
        }

        public Task Send (Message model)
        {
			string expression = @"INSERT INTO messages (id, fromuserid, touserid, content, createdat) 
								VALUES (@id, @fromuserid, @touserid, @content, @createdat)";

			NpgsqlParameter[] parameters = new[]
			{
				new NpgsqlParameter(nameof(model.Id), model.Id),
				new NpgsqlParameter(nameof(model.FromUserId), model.FromUserId),
				new NpgsqlParameter(nameof(model.ToUserId), model.ToUserId),
				new NpgsqlParameter(nameof(model.Content), model.Content),
				new NpgsqlParameter(nameof(model.CreatedAt), model.CreatedAt),
			};
			return _dbConnection.Add(expression, parameters);
		}

		public Task<List<Message>> GetMessages(Guid fromuserid, Guid touserid)
		{
			string expression = @"SELECT * FROM messages WHERE (fromuserid = @fromuserid AND touserid = @touserid) OR (fromuserid = @touserid AND touserid = @fromuserid) ";

			NpgsqlParameter[] parameters = new[]
			{
				new NpgsqlParameter(nameof(fromuserid), fromuserid),
				new NpgsqlParameter(nameof(touserid), touserid),
			};
			return _dbConnection.GetList(expression, ChatConverter.MapToMessages, parameters);
		}

	}
}
