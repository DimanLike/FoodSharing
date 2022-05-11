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
        public async Task<ActionResult> Profile(UserProfileViewModel model)
        {
            string claim = User.Identity.Name;
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

            string email = User.Identity.Name;
            UserProfileViewModel user = await _userService.GetUserDataProfile(email);
            model.Id = user.Id;

            await _userService.AddUserDataProfile(model);

            return View("Profile", model);
        }

        [HttpPost]
        public async Task<ActionResult> SavePhoto(UserProfileViewModel model)
        {
            UserAvatar useravatar = new UserAvatar();
            if (model.Avatar != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
                }
               model.Image = imageData;
            }
            
            return View("Profile", model);

        }





    }
}
