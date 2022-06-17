using FoodSharing.Models.Chat;
using FoodSharing.Services.Chat.Interfaces;
using FoodSharing.Services.Users.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodSharing.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IConfiguration _config;
        private IChatService _chatService;
        private IUserService _userService;

        public ChatController(IConfiguration config, IChatService chatService, IUserService userService)
        {
            _config = config;
            _chatService = chatService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ChatMessage(Guid userId)
        {
            if (userId == Guid.Empty) return View();

            MessagesHistoryView messegesHistory = await _chatService.GetMessagesHistory(userId, User.Identity.Name);

            return View(messegesHistory);
        }

        public async Task<IActionResult> ChatUsers()
        {           
            Guid UserId = await _userService.GetUserIdByEmail(User.Identity.Name);
            List<MessagesHistoryView> messages = await _chatService.GetTalkers(UserId);
            return View(messages);
        }


    }
}