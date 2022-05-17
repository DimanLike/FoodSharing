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

        [HttpGet]
        [Route("/User/NewProduct")]
        public async Task<IActionResult> NewProduct()
        {
            ProductsViewModel model = new ProductsViewModel();
            List<ProductCategories> productCategories = await _userService.GetCategories();
            model.Categories = productCategories;

            return View(model);
        }

        public  IActionResult Products()
        {
            return View();

        }

        public async Task<ActionResult> GetInventoryProduct(ProductsViewModel model)
		{
			string email = User.Identity.Name;
			User user = await _userService.GetUserByEmailAndPassword(email);
            var userid = user.Id;

            List<ProductsViewModel> UserProducts = await _userService.GetUserInventory(userid);

            return View("Products", UserProducts);

        }

        [HttpPost]
        [Route("/User/NewProduct")]
        public async Task<ActionResult> NewProduct(ProductsViewModel model)
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
                return View("NewProduct", model);
            }

            TempData["AddProductSeccess"] = "Товар был добавлен";
            return RedirectToAction("NewProduct", "User");
        }
    }
}
