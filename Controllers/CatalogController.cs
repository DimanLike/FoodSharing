using FoodSharing.Models;
using FoodSharing.Models.Users;
using FoodSharing.Services.Products.Interfaces;
using FoodSharing.Services.Users.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodSharing.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IConfiguration _config;
        private IUserService _userService;
        private IProductService _productService;


        public CatalogController(IConfiguration config, IUserService userService, IProductService productService )
        {
            _config = config;
            _userService = userService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> ProductInfo(Guid id)
		{
            ProductInfoViewModel model = new ProductInfoViewModel();
            UserProfileViewModel userProfileViewModel = new UserProfileViewModel();
            ProductView productView = new ProductView();

            productView = await _productService.GetProduct(id);
            userProfileViewModel = await _userService.GetUserProfile(productView.UserId);

            model.ProductView = productView;
            model.UserProfileViewModel = userProfileViewModel;

            return View(model);
        }


    }
}
