using FoodSharing.Models;
using FoodSharing.Models.Products;
using FoodSharing.Models.Users;
using FoodSharing.Services.Users.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodSharing.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _config;
        private IUserService _userService;

        public UserController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        // пока не придумал для чего этот контроллер может понадобиться
    }
}
