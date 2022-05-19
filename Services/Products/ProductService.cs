using AutoMapper;
using FoodSharing.Models;
using FoodSharing.Models.Products;
using FoodSharing.Models.Products.ProductCategories;
using FoodSharing.Services.Products.Interfaces;

namespace FoodSharing.Services.Products
{
    public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;

		public ProductService(IProductRepository productRepository)
		{
			_productRepository = productRepository;
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
			// следи за тем, что возвращаешь из методов, здесь null не подходит логически
			// возвращаемый тип - List<ProductView>, значит должен в любом случае вернуться лист, поэтому возвращаем пустой здесь
			if (products is null) return new List<ProductView>();

			int[] categoryIds = products.Select(x => x.CategoryId).ToArray();
			List<ProductCategory> productCategories = await _productRepository.GetProductCategories(categoryIds);

			return products.Select(x =>
			{
				// сделал категорию nullable
				ProductCategory? productCategory = productCategories.FirstOrDefault(c => c.Id == x.CategoryId);

				// лучше экономить вертикальное место, лучше смотрится визуально
				return new ProductView(x.Id, x.UserId, x.Name, x.Description, x.CategoryId,
					// чтобы не упало вдруг с ошибкой приложения, сделал проверку - если productCategory будет null, то подставить пустую строку
					// потому что если написать productCategory?.Name ?? null, то будет подчеркивать зеленым
					productCategory?.Name ?? "", x.Quantity, x.Image, x.CreatedAt);
			}).ToList();
			//var configuration = new MapperConfiguration(cfg =>
			//{
			//    cfg.CreateMap<UserProducts, ProductsViewModel>();
			//});
			//var mapper = configuration.CreateMapper();

			//var productsViewModel = mapper.Map<List<UserProducts>, List<ProductsViewModel>> (userProducts);
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

		public Task EditProduct(ProductView model)
        {
			return _productRepository.EditProduct(model);
        }

		// называй конкретнее методы
		public async Task<List<ProductCategory>> GetProductCategories()
		{
			return await _productRepository.GetProductCategories();
		}
	}
}