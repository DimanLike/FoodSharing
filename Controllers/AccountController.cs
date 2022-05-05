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
        public IActionResult Profile()
        {
            return View();
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
            


            var form = HttpContext.Request.Form;

            Guid id = user.Id;
            string firstname = form["firstname"];
            string lastname = form["lastname"];
            string email = form["email"];
            string adress = form["adress"];
            string phone = form["phone"];

            await _userService.AddUserDataProfile(id, firstname, lastname, email, adress, phone);

            //await _userService.AddUserDataProfile()
            return View();
        }
        

    }
}
