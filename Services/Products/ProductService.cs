using AutoMapper;
using FoodSharing.Models;
using FoodSharing.Models.Products;
using FoodSharing.Models.Products.ProductCategories;
using FoodSharing.Models.Users;
using FoodSharing.Services.Products.Interfaces;
using FoodSharing.Services.Users.Interfaces;

namespace FoodSharing.Services.Products
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;
		private readonly IUserService _userService;

		public ProductService(IProductRepository productRepository, IUserService userService)
		{
			_productRepository = productRepository;
			_userService = userService;
		}

		public Task AddProduct(ProductView model)
		{
			return _productRepository.AddProduct(model);
		}

		public Task DeleteProduct(Guid id)
		{
			return _productRepository.DeleteProduct(id);
		}

		public async Task<List<ProductView>> GetProductsViews(Guid userid)
		{
			List<Product> products = await _productRepository.GetProducts(userid);
			if (products is null) return new List<ProductView>();

			int[] categoryIds = products.Select(x => x.CategoryId).ToArray();
			List<ProductCategory> productCategories = await _productRepository.GetProductCategories(categoryIds);

			return products.Select(x =>
			{
				ProductCategory? productCategory = productCategories.FirstOrDefault(c => c.Id == x.CategoryId);

				return new ProductView(x.Id, x.UserId, x.Name, x.Description, x.CategoryId,

					productCategory?.Name ?? "", x.Quantity, x.Image, x.CreatedAt);
			}).ToList();

		}

		public async Task<List<CatalogView>> GetCatalogViews(int categoryId = default)
		{

			List<Product> products = new List<Product>();

			if (categoryId == null || categoryId == 0)
			{
				products = await _productRepository.GetCatalog();
				
			}
			else
			{
				products = await _productRepository.GetCatalog(categoryId);
			}

			if (products is null) return new List<CatalogView>();

			int[] categoryIds = products.Select(x => x.CategoryId).ToArray();
			Guid[] userIds = products.Select(x => x.UserId).ToArray();
			List<ProductCategory> productCategories = new List<ProductCategory>();
			productCategories = await _productRepository.GetProductCategories(categoryIds);
			
            List<User> Users = await _userService.GetUsers(userIds);

			return products.Select(x =>
			{
				ProductCategory? productCategory = productCategories.FirstOrDefault(c => c.Id == x.CategoryId);
				User? user = Users.FirstOrDefault(c => c.Id == x.UserId);
				return new CatalogView(x.Id, x.UserId, user?.Email ?? "", x.Name, x.Description, x.CategoryId, productCategory?.Name ?? "", x.Quantity, x.Image, x.CreatedAt);
			}).ToList();
		}

		public async Task<ProductView> GetProduct(Guid productid)
		{
			Product product = await _productRepository.GetProduct(productid);
			ProductCategory productCategory = await _productRepository.GetProductCategory(product.CategoryId);
			if (product is null) return null;

			var configuration = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<Product, ProductView>();
			});
			var mapper = configuration.CreateMapper();

			var productsViewModel = mapper.Map<Product, ProductView>(product);

			productsViewModel.CategoryName = productCategory.Name;

			return productsViewModel;

		}

		public async Task EditProduct(ProductView model)
		{
			Product product = await _productRepository.GetProduct(model.Id);
			model.Image = product.Image;
			await _productRepository.EditProduct(model);
		}

		public async Task<List<ProductCategory>> GetProductCategories()
		{
			return await _productRepository.GetProductCategories();
		}
	}
}