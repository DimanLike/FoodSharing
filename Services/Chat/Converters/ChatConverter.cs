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
	}
}
