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
			UserProfileView userProfile = await _userService.GetUserProfile(User.Identity.Name);

			return View("Profile", userProfile);
		}

        [HttpGet]
		public async Task<ActionResult> ProfileInfo(Guid userid)
        {
			ProfileInfoView profileInfoView = await _userService.GetUserProfileInfo(userid);

			return View(profileInfoView);
        }

		[HttpPost]
		public async Task<ActionResult> EditProfile(UserProfileView model)
		{
			if (!ModelState.IsValid) return View();

			await _userService.SaveUserProfile(model, User.Identity.Name);
			TempData["SaveChanges"] = "Изменения были применены";
	
			return RedirectToAction("Profile", "Account");
		}

		[HttpPost]
		public async Task<ActionResult> SavePhoto(UserProfileView model)
		{
			string email = User.Identity.Name;
			UserProfileView userProfile = await _userService.GetUserProfile(email);
			model.Id = userProfile.Id;

			if (model.Image is null)
			{
				model.Avatar = userProfile.Avatar;

				TempData["UploadPhotoError"] = "Фото не было загружено";
				return View("Profile", model);
			}

			await _userService.SaveUserProfile(model, email);

			TempData["SaveChanges"] = "Изменения были применены";
			return RedirectToAction("Profile", "Profile");
		}
	}
}