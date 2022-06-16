using FoodSharing.Models.Chat;
using FoodSharing.Services.Chat.Interfaces;
using FoodSharing.Services.Users.Interfaces;

namespace FoodSharing.Services.Chat
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private IUserService _userService;

        public ChatService(IChatRepository chatRepository, IUserService userService)
        {
            _chatRepository = chatRepository;
            _userService = userService;
        }

        public Task Send(Message model)
        {
            return _chatRepository.Send(model);
        }

        public async Task<List<MessageView>> GetMessages(Guid fromuserid, Guid touserid)
        {
            List<Message> messages = await _chatRepository.GetMessages(fromuserid, touserid);
            if (messages.Count == 0) return new List<MessageView>();

            return messages.Select(x =>
            {
                Task<string> toUserEmail = _userService.GetUserEmailById(x.ToUserId);
                Task<string> fromUserEmail = _userService.GetUserEmailById(x.FromUserId);

                return new MessageView(x.Id, x.FromUserId, fromUserEmail.Result, x.ToUserId, toUserEmail.Result, x.Content, x.CreatedAt);
            }).ToList();
        }

        public async Task<MessagesHistoryView> GetMessagesHistory(Guid userId, string email)
        {
            MessagesHistoryView messegesHistory = new MessagesHistoryView();

            messegesHistory.FromUserId = await _userService.GetUserIdByEmail(email);
            messegesHistory.ToUserId = userId;

            messegesHistory.FromUserAvatar = await _userService.GetAvatar(messegesHistory.FromUserId);
            messegesHistory.ToUserAvatar = await _userService.GetAvatar(messegesHistory.ToUserId);

            messegesHistory.Messages = (await GetMessages(messegesHistory.FromUserId, messegesHistory.ToUserId)).OrderBy(x => x.CreatedAt).ToList();

            return messegesHistory;
        }
    }
}
