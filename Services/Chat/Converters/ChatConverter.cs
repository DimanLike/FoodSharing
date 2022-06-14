using FoodSharing.Models.Chat;
using Npgsql;

namespace FoodSharing.Services.Chat.Converters
{
    public class ChatConverter
    {

		public static async Task<List<Messages>> MapToMessages(NpgsqlDataReader reader)
		{
			List<Messages> messages = new List<Messages>();

			if (reader.HasRows)
			{
				while (await reader.ReadAsync())
				{
					Messages message = new Messages();

					message.Id = (Guid)reader["Id"];
					message.FromUserId = (Guid)reader["FromUserId"];
					message.ToUserId = (Guid)reader["ToUserId"];
					message.Content = (string)reader["Content"];
					message.CreatedAt = (DateTime)reader["CreatedAt"];

					messages.Add(message);
				}
			}
			else
			{
				return new List<Messages>();
			}

			return messages;
		}
	}
}
