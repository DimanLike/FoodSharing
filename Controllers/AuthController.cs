using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FoodSharing.Models;
using FoodSharing.Services.Users.Interfaces;
using FoodSharing.Models.Users;
using FoodSharing.Tools.Authorization;

namespace FoodSharing.Controllers
{
    [AllowAnonymous]
	public class AuthController : Controller
	{
		private readonly IConfiguration _config;
        private IUserService _userService;

        public AuthController(IConfiguration config, IUserService userService)
		{
			_config = config;
            _userService = userService;
        }

		[HttpGet]
		[Route("Login")]
		public IActionResult Login()
		{
			return View();
		}

		[HttpGet]
		[Route("Registration")]
		public IActionResult Registration()
		{
			return View();
		}

		[HttpPost]
		[Route("Login")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Login(LoginViewModel model)
		{
			if (!ModelState.IsValid) return View("Login", model);

			User user = await _userService.GetUserByEmail(model.Email); 
			if (user is null)
            {
				TempData["LoginError"] = "Пользователь не найден";
				return View(model);
			}	

			await CookieEvents.Authenticate(model.Email, HttpContext);

			return RedirectToAction("Profile", "Account");
		}

		[Authorize]
		public async Task<IActionResult> LogOut()
		{
			await CookieEvents.SignOut(HttpContext);

			return RedirectToAction("Login", "Auth");
		}

		[HttpPost]
		[Route("RegisterUser")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> RegisterUser(RegistrationView model)
		{
			if (!ModelState.IsValid) return View("Registration", model);

			User user = await _userService.GetUserByEmail(model.Email);
			
			if (user is null)
			{
				await _userService.RegisterUser(model);
				await CookieEvents.Authenticate(model.Email, HttpContext);

				return RedirectToAction("Profile", "Account");
			}
			else
			{ 
				TempData["RegisterError"] = "Пользователь с данной почтой уже существует";
				return View("Registration", model);
			}
		}
	}
}
