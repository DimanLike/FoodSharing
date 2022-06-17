using FoodSharing.Models.Chat;
using FoodSharing.Services.Chat.Interfaces;
using FoodSharing.Services.Users.Interfaces;
using System.Linq;

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

        public async Task<MessageView> GetMessage(Guid fromuserid, Guid touserid)
        {
            List<Message> messages = await _chatRepository.GetMessages(fromuserid, touserid);

            Message message = messages.OrderBy(x => x.CreatedAt).Last();

            string toUserEmail = await _userService.GetUserEmailById(message.ToUserId);
            string fromUserEmail = await _userService.GetUserEmailById(message.FromUserId);

            MessageView messageView = new MessageView(message.Id, message.FromUserId, fromUserEmail, message.ToUserId, toUserEmail, message.Content, message.CreatedAt);

            if (messages.Count == 0) return new MessageView();
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

        public async Task<MessageHistoryView> GetLastMessage(Guid userId, string email)
        {
            MessageHistoryView messegesHistory = new MessageHistoryView();

            messegesHistory.FromUserId = await _userService.GetUserIdByEmail(email);
            messegesHistory.ToUserId = userId;

            messegesHistory.FromUserAvatar = await _userService.GetAvatar(messegesHistory.FromUserId);
            messegesHistory.ToUserAvatar = await _userService.GetAvatar(messegesHistory.ToUserId);

            messegesHistory.Messages = (await GetMessages(messegesHistory.FromUserId, messegesHistory.ToUserId)).OrderBy(x => x.CreatedAt).ToList();
            messegesHistory.Messages = messegesHistory.Messages.Last();

            return messegesHistory;
        }

        public async Task<List<MessagesHistoryView>> GetTalkers(Guid userid)
        {
            List<Guid> userids = await _chatRepository.GetTalkers(userid);
            string email = await _userService.GetUserEmailById(userid);
            List<MessagesHistoryView> messages = new List<MessagesHistoryView>();

           foreach (Guid user in userids)
            {
                MessagesHistoryView messagesuser = await GetMessagesHistory(user, email);
                messages.Add(messagesuser);
            }
            if (messages is null)
            {
                return new List<MessagesHistoryView>();
            }
                 else
            {
                return messages;
            }

        }
    }
}
