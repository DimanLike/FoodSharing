using FoodSharing.Models.Chat;

namespace FoodSharing.Services.Chat.Interfaces
{
    public interface IChatRepository
    {
        Task<List<Message>> GetMessages(Guid fromuserid, Guid touserid);

        Task Send(Message model);

        Task<List<Guid>> GetTalkers(Guid userid);

        Task<List<Message>> GetAllMessages(Guid fromuserid);

        Task<List<Guid>> GetToTalkers(Guid userid);
        Task<List<Guid>> GetFromTalkers(Guid userid);
    }

}
