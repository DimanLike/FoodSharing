using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodSharing.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        [Route("Profile")]
        public IActionResult Profile()
        {
            return View();
        }
    }
}
