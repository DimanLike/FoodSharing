using FoodSharing.Models.Chat;
using Npgsql;

namespace FoodSharing.Services.Chat.Converters
{
    public class ChatConverter
    {
		public static async Task<List<Message>> MapToMessages(NpgsqlDataReader reader)
		{
			List<Message> messages = new List<Message>();

			if (reader.HasRows)
			{
				while (await reader.ReadAsync())
				{
					Message message = new Message();

					message.Id = (Guid)reader["Id"];
					message.FromUserId = (Guid)reader["FromUserId"];
					message.ToUserId = (Guid)reader["ToUserId"];
					message.Content = (string)reader["Content"];
					message.CreatedAt = (DateTime)reader["CreatedAt"];

					messages.Add(message);
				}
			}

			return messages;
		}

		public static async Task<List<Guid>> MapToGuid(NpgsqlDataReader reader)
        {
			List<Guid> guids = new List<Guid>();

			while (await reader.ReadAsync())
            {
				Guid guid = new Guid();
				guid = (Guid)reader["ToUserId"];

				guids.Add(guid);
			}

			return guids;
		}

		public static async Task<List<Guid>> MapFromGuid(NpgsqlDataReader reader )
		{
			List<Guid> guids = new List<Guid>();

			while (await reader.ReadAsync())
			{
				Guid guid = new Guid();
				guid = (Guid)reader["FromUserId"];

				guids.Add(guid);
			}

			return guids;
		}
	}
}
