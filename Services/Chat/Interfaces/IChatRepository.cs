using FoodSharing.Models.Chat;

namespace FoodSharing.Services.Chat.Interfaces
{
    public interface IChatRepository
    {
        Task<List<Messages>> GetMessages(Guid fromuserid, Guid touserid);

        Task Send(Messages model);

    }

}
