﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using FoodSharing.Models;
using FoodSharing.Services.Users.Interfaces;
using FoodSharing.Models.Users;

namespace FoodSharing.Controllers
{

	record class Person(string Email, string Password);

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
		public async Task<ActionResult> Login(LoginViewModel model, string? returnUrl)
		{
			if (!ModelState.IsValid)
			{
				return View("Login", model);
			}

			var form = HttpContext.Request.Form;

			if (!form.ContainsKey("email") || !form.ContainsKey("password"))
				return View();

			string email = form["email"];
			string password = form["password"];

			User user = await _userService.GetUserByEmailAndPassword(email, password); 
			if (user is null)
            {
				TempData["LoginError"] = "Пользователь не найден";
				return View(model);
            }	

			await Authenticate(model.Email);

			return RedirectToAction("Profile", "Account");
			
		}

		[Authorize]
		public async Task<IActionResult> LogOut()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			return RedirectToAction("Login", "Auth");
		}

		[HttpPost]
		[Route("RegisterUser")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> RegisterUser(RegistrationViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View("Registration", model);
			}
			var form = HttpContext.Request.Form;
			string email = form["email"];
			string password = form["password"];

			await _userService.AddUserByEmailAndPassword(email, password);

			await Authenticate(model.Email);

			return RedirectToAction("Profile", "Account");
		}

		private async Task Authenticate(string username)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, username)
			};

			ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
 
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
		}
	}
}
