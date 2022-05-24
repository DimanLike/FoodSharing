using FoodSharing.Models;
using FoodSharing.Models.Users;
using FoodSharing.Services.Users.Interfaces;
using FoodSharing.Tools;
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
			string email = User.Identity.Name;
			User user = await _userService.GetUserByEmail(email);
			UserProfileViewModel userProfile = await _userService.GetUserProfile(user.Id);

			return View("Profile", userProfile);
		}


		[HttpPost]
		public async Task<ActionResult> EditProfile(UserProfileViewModel model)
		{
			if (!ModelState.IsValid) return View();

			string email = User.Identity.Name;
			User user = await _userService.GetUserByEmail(email);
			UserProfileViewModel userProfile = await _userService.GetUserProfile(user.Id);
			model.Id = userProfile.Id;
			if (model.Image != null)
			{
				model.Avatar = FileTools.GetBytes(model.Image);
			}
			else
			{
				model.Avatar = userProfile.Avatar;
			}

			await _userService.AddUserProfile(model);
			TempData["SaveChanges"] = "Изменения были применены";
	
			return RedirectToAction("Profile", "Account");
		}

		[HttpPost]
		public async Task<ActionResult> SavePhoto(UserProfileViewModel model)
		{
			string email = User.Identity.Name;
			User user = await _userService.GetUserByEmail(email);
			UserProfileViewModel userProfile = await _userService.GetUserProfile(user.Id);
			model.Id = userProfile.Id;

			if (model.Image != null)
			{
				model.Avatar = FileTools.GetBytes(model.Image);

				await _userService.AddUserProfile(model);
			}
			else
			{
				TempData["UploadPhotoError"] = "Фото не было загружено";
				model.Avatar = userProfile.Avatar;
				return View("Profile", model);
			}
			TempData["SaveChanges"] = "Изменения были применены";
			//return View(model);
			return RedirectToAction("Profile", "Profile");
		}
	}
}