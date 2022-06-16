using FoodSharing.Models.Chat;

namespace FoodSharing.Services.Chat.Interfaces
{
    public interface IChatService
    {
        Task Send(Message model);

        Task<List<MessageView>> GetMessages(Guid fromuserid, Guid touserid);
        Task<MessagesHistoryView> GetMessagesHistory(Guid userId, string email);
    }
}
