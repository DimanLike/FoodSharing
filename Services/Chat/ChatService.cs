using FoodSharing.Models.Chat;
using FoodSharing.Services.Chat.Interfaces;

namespace FoodSharing.Services.Chat
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;

        public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public Task Send(Messages model)
        {
            return _chatRepository.Send(model);
        }

        public async Task<List<Messages>> GetMessages(Guid fromuserid, Guid touserid)
        {
            return await _chatRepository.GetMessages(fromuserid, touserid);
        }
    }
}
