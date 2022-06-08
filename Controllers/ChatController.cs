using FoodSharing.Models;
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

        public ChatController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ChatMessage( Guid userId)
        {
            ProfileInfoView profileInfoView = await _userService.GetUserProfileInfo(userId);


            return View(profileInfoView);
        }


    }
}
