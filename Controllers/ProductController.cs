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
			User user = await _userService.GetUserByEmail(User.Identity.Name);

			List<ProductView> productViews = await _productService.GetProductsViews(user.Id);

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

		[HttpGet]
		[Route("/Products/Favourites")]
		public async Task<ActionResult> Favourites()
		{
			string email = User.Identity.Name;
			Guid userId = await _userService.GetUserIdByEmail(email);
			List<ProductView> productViews = await _productService.GetProductsFavouritesViews(userId);
			return View(productViews);
		}

		[HttpPost]
		[Route("/Products/NewProduct")]
		public async Task<ActionResult> NewProduct(ProductView model)
		{
			if (model.IFormFile is null)
			{
				TempData["UploadPhotoError"] = "Фото не было загружено";
				return View("NewProduct", model);
			}

			User user = await _userService.GetUserByEmail(User.Identity.Name);
			model.UserId = user.Id;

			await _productService.SaveProduct(model);

			TempData["AddProductSeccess"] = "Товар был добавлен";
			return RedirectToAction("NewProduct", "Product");
		}

		[HttpGet]
		[Route("/Products/EditProduct")]
		public async Task<ActionResult> EditProduct(Guid id)
		{
			User user = await _userService.GetUserByEmail(User.Identity.Name);
			List<ProductCategory> productCategories = await _productService.GetProductCategories();

			ProductView model = await _productService.GetProduct(id);
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

			string email = User.Identity.Name;
			Guid currentUserId = await _userService.GetUserIdByEmail(email);

			catalogListView.CatalogViews = await _productService.GetCatalogViews(model.CategoryId, currentUserId);
			catalogListView.ProductCategories = await _productService.GetProductCategories();

			return View("Сatalog", catalogListView);
        }

		[HttpGet]
		public async Task<ActionResult> ChangeProductFavourites(Guid productid)
		{
			string email = User.Identity.Name;
			Guid userId = await _userService.GetUserIdByEmail(email);

			Guid userProfileId = await _userService.GetUserIdByProductId(productid);
			_productService.ChangeProductFavourite(userId, productid);

			return RedirectToAction("Сatalog", "Product");
		}


		[HttpGet]
		public async Task<ActionResult> ProductInfo(Guid id)
		{
			ProductView productView = await _productService.GetProduct(id);
			UserProfileView userProfileViewModel = await _userService.GetUserProfile(productView.UserId);

			ProductInfoView model = new ProductInfoView(userProfileViewModel, productView);

			return View(model);
		}
	}
}
