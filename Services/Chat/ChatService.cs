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

        public async Task<Dialog> GetDialog(Guid fromuserid, Guid touserid)
        {
            List<Message> messages = await _chatRepository.GetMessages(fromuserid, touserid);

            Message message = messages.OrderBy(x => x.CreatedAt).Last();

            string toUserEmail = await _userService.GetUserEmailById(message.ToUserId);
            string fromUserEmail = await _userService.GetUserEmailById(message.FromUserId);

            byte[] toUserAvatar = await _userService.GetAvatar(message.ToUserId);

            byte[] fromUserAvatar = await _userService.GetAvatar(message.FromUserId);

            Dialog dialog = new Dialog(message.Id, message.FromUserId, fromUserEmail, fromUserAvatar, message.ToUserId, toUserEmail, toUserAvatar, message.Content, message.CreatedAt);

            if (dialog is null)
                return new Dialog();
            else
                return dialog;
        }

        public async Task<AllDialogsView> GetTalkers(Guid userid)
        {
            List<Guid> userids = await GetTalkersId(userid);
            string email = await _userService.GetUserEmailById(userid);

            AllDialogsView allDialogs = new AllDialogsView();

            foreach (Guid user in userids)
            {
                Dialog dialog = await GetDialog(user, userid);
                if (allDialogs.Dialog is null)
                    allDialogs.Dialog = new List<Dialog>() { dialog };
                else
                    allDialogs.Dialog.Add(dialog);
            }
            if (allDialogs is null)
            {
                return new AllDialogsView();
            }
            else
            {
                return allDialogs;
            }

        }

        public async Task<List<Guid>> GetTalkersId(Guid userid)
        {
            List<Message> messages = await _chatRepository.GetAllMessages(userid);

            List<Guid> toTalkers = await _chatRepository.GetToTalkers(userid);
            List<Guid> fromTalkers = await _chatRepository.GetFromTalkers(userid);

            foreach (Guid p in toTalkers)
                fromTalkers.Add(p);

            List<Guid> Talkers = fromTalkers.Distinct().ToList();

            return Talkers;
        }

    }
}
