using FoodSharing.Models;
using FoodSharing.Models.Users;
using FoodSharing.Services.Users.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace FoodSharing.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IConfiguration _config;
        private IUserService _userService;

        public AccountController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }


        [Route("Profile")]
        public async Task<ActionResult> Profile()
        {
            string claim = User.Identity.Name;
            //UserProfile user = await _userService.GetUserDataProfile(claim);
            UserProfileViewModel userProfile = await _userService.GetUserDataProfile(claim);

            return View(userProfile);
        }

        [HttpPost]
        public async Task<ActionResult> EditProfile(UserProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            string claim = User.Identity.Name;
            User user = await _userService.GetUserByEmailAndPassword(claim);
            model.Id = user.Id;

            await _userService.AddUserDataProfile(model);

            return View("Profile", model);
        }

        [HttpPost]
        public async Task<ActionResult> SavePhoto(UserAvatarViewModel model)
        {
            return View("Profile");

        }





    }
}
