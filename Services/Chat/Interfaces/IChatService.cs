using FoodSharing.Models.Chat;

namespace FoodSharing.Services.Chat.Interfaces
{
    public interface IChatService
    {
        Task Send(Messages model);

        Task<List<Messages>> GetMessages(Guid fromuserid, Guid touserid);
    }
}
