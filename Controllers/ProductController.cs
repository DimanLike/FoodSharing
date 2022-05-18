using FoodSharing.Models;
using FoodSharing.Models.Products.ProductCategories;
using FoodSharing.Models.Users;
using FoodSharing.Services.Products.Interfaces;
using FoodSharing.Services.Users.Interfaces;
using FoodSharing.Tools;
using Microsoft.AspNetCore.Mvc;

namespace FoodSharing.Controllers
{
    public class ProductController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IProductService _productService;
        private readonly IUserService _userService;

        public ProductController(IConfiguration config, IProductService productService, IUserService userService)
        {
            _config = config;
            _productService = productService;
            _userService = userService;
        }

        public IActionResult Products()
        {
            return RedirectToAction("GetProducts", "Product");
        }

        public async Task<ActionResult> GetProducts(ProductView model)
        {
            string email = User.Identity.Name;

            User user = await _userService.GetUserByEmailAndPassword(email);
            Guid userId = user.Id;

            List<ProductView> productViews = await _productService.GetProductViews(userId);

            return View("Products", productViews);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteProduct(Guid id)
        {
            await _productService.DeleteProduct(id);

            return RedirectToAction("GetProducts", "Product");
        }

        [HttpGet]
        [Route("/Products/NewProduct")]
        public async Task<ActionResult> NewProduct()
        {
            ProductView model = new ProductView();
            List<ProductCategory> productCategories = await _productService.GetProductCategories();

            model.ProductCategories = productCategories;

            return View(model);
        }

        [HttpPost]
        [Route("/Products/NewProduct")]
        public async Task<ActionResult> NewProduct(ProductView model)
        {
            string email = User.Identity.Name;

            User user = await _userService.GetUserByEmailAndPassword(email);
            model.UserId = user.Id;

            if (model.IFormFile != null)
            {
                model.Image = FileTools.GetBytes(model.IFormFile);

                await _productService.AddProduct(model);
            }
            else
            {
                TempData["UploadPhotoError"] = "Фото не было загружено";
                return View("NewProduct", model);
            }

            TempData["AddProductSeccess"] = "Товар был добавлен";
            return RedirectToAction("NewProduct", "Product");
        }
    }
}
