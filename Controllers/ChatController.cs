using FoodSharing.Models.Chat;
using FoodSharing.Services.Chat.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodSharing.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IConfiguration _config;
        private IChatService _chatService;

        public ChatController(IConfiguration config, IChatService chatService)
        {
            _config = config;
            _chatService = chatService;
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
    }
}