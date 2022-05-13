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

        public IActionResult NewProduct()
        {
            return View();
        }

        public IActionResult Products()
        {
            return View();
        }

        public async Task<ActionResult> AddProduct(ProductsViewModel model)
        {
            string email = User.Identity.Name;
            User user = await _userService.GetUserByEmailAndPassword(email);
            model.UserId = user.Id;

            if (model.IFormFile != null)
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(model.IFormFile.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)model.IFormFile.Length);
                }
                model.Image = imageData;

                await _userService.AddNewUserProduct(model);

            }
            else
            {
                TempData["UploadPhotoError"] = "Фото не было загружено";
                return View(model);
            }

            return View(model);


            return View();
        }
    }
}
