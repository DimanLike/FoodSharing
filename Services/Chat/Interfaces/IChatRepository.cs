using FoodSharing.Models.Chat;

namespace FoodSharing.Services.Chat.Interfaces
{
    public interface IChatRepository
    {
        Task<List<Message>> GetMessages(Guid fromuserid, Guid touserid);

        Task Send(Message model);

    }

}
