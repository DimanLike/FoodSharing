using FoodSharing.Models;
using FoodSharing.Models.Products;
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

			User user = await _userService.GetUserByEmail(email);
			Guid userId = user.Id;

			List<ProductView> productViews = await _productService.GetProductsViews(userId);

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

			User user = await _userService.GetUserByEmail(email);
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

		[HttpGet]
		[Route("/Products/EditProduct")]
		public async Task<ActionResult> EditProduct(Guid id)
		{
			string email = User.Identity.Name;
			ProductView model = new ProductView();

			User user = await _userService.GetUserByEmail(email);
			List<ProductCategory> productCategories = await _productService.GetProductCategories();

			model = await _productService.GetProduct(id);
			model.ProductCategories = productCategories;
			model.UserId = user.Id;

			return View(model);
		}

        [HttpPost]
		[Route("/Products/EditProduct")]
		public async Task<ActionResult> EditProduct(ProductView model)
        {
			if (model.Image is not null)
            {
				model.Image = FileTools.GetBytes(model.IFormFile);
            }
			await _productService.EditProduct(model);
			return RedirectToAction("GetProducts", "Product");
        }

		[HttpGet]
		public IActionResult Сatalog()
		{
			return RedirectToAction("GetCatalog", "Product");
		}

		[Route("/Products/Сatalog")]
		public async Task<ActionResult> GetCatalog(CatalogListView model)
        {
			CatalogListView catalogListView = new CatalogListView();

			catalogListView.CatalogViews = await _productService.GetCatalogViews(model.CategoryId);
			catalogListView.ProductCategories = await _productService.GetProductCategories();


			return View("Сatalog", catalogListView);

        }

		[HttpGet]
		public async Task<ActionResult> ProductInfo(Guid id)
		{
			ProductInfoView model = new ProductInfoView();
			UserProfileView userProfileViewModel = new UserProfileView();
			ProductView productView = new ProductView();

			productView = await _productService.GetProduct(id);
			userProfileViewModel = await _userService.GetUserProfile(productView.UserId);

			model.ProductView = productView;
			model.UserProfileViewModel = userProfileViewModel;

			return View(model);
		}


	}
}
