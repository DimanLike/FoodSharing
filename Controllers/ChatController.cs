using FoodSharing.Models;
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
        private IUserService _userService;
        private IChatService _chatService;

        public ChatController(IConfiguration config, IUserService userService, IChatService chatService)
        {
            _config = config;
            _userService = userService;
            _chatService = chatService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ChatMessage(Guid userId)
        {
            if (userId != Guid.Empty)
            {
                MessegesHistoryView messegesHistory = new MessegesHistoryView();
                messegesHistory.FromUserId = await _userService.GetUserIdByEmail(User.Identity.Name);
                messegesHistory.FromUser = User.Identity.Name;
                messegesHistory.ToUserId = userId;
                messegesHistory.ToUser = await _userService.GetUserEmailById(userId);

                messegesHistory.Messages = await _chatService.GetMessages(messegesHistory.FromUserId, messegesHistory.ToUserId);
                messegesHistory.Messages.OrderBy(x => x.CreatedAt).ToList();

                return View(messegesHistory);
                
            }

            return View();

        }


    }
}
