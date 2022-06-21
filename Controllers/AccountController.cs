using FoodSharing.Models;
using FoodSharing.Models.Users;
using FoodSharing.Services.Products.Interfaces;
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
		private IProductService _productService;

		public AccountController(IConfiguration config, IUserService userService, IProductService productService)
		{
			_config = config;
			_userService = userService;
			_productService = productService;
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
			string email = User.Identity.Name;
			Guid currentUserId = await _userService.GetUserIdByEmail(email);

			ProfileInfoView profileInfoView = await _userService.GetUserProfileInfo(userid, currentUserId);

			return View(profileInfoView);
        }

		[HttpGet]
		public async Task<ActionResult> ChangeProductFavourites(Guid productid)
		{
			string email = User.Identity.Name;
			Guid userId = await _userService.GetUserIdByEmail(email);

			Guid userProfileId = await _userService.GetUserIdByProductId(productid);
			_productService.ChangeProductFavourite(userId, productid);

			return RedirectToAction("ProfileInfo", "Account", new { userid = userProfileId });
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