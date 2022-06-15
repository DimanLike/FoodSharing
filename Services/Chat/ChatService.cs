using FoodSharing.Models.Chat;
using FoodSharing.Services.Chat.Interfaces;
using FoodSharing.Services.Users.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        public Task Send(Messages model)
        {
            return _chatRepository.Send(model);
        }

        public async Task<List<MessagesView>> GetMessages(Guid fromuserid, Guid touserid)
        {
            List<Messages> messages = await _chatRepository.GetMessages(fromuserid, touserid);
            if (messages is null) return new List<MessagesView>();

            return messages.Select(x =>
            {
                Task<string> toUserEmail = _userService.GetUserEmailById(x.ToUserId);
                Task<string> fromUserEmail = _userService.GetUserEmailById(x.FromUserId);

                return new MessagesView(x.Id, x.FromUserId, fromUserEmail.Result, x.ToUserId, toUserEmail.Result, x.Content, x.CreatedAt);
            }).ToList();
        }
    }
}
