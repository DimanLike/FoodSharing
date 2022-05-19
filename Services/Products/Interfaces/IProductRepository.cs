using FoodSharing.Models;
using FoodSharing.Models.Products;
using FoodSharing.Models.Products.ProductCategories;

namespace FoodSharing.Services.Products.Interfaces
{
    public interface IProductRepository
    {
        // Products
        Task AddProduct(ProductView model);
        Task DeleteProduct(Guid id);
        Task<List<Product>> GetProducts(Guid userid);
        Task<Product> GetProduct(Guid id);
        Task EditProduct(ProductView model);

        // ProductCategories
        Task<List<ProductCategory>> GetProductCategories();
        Task<List<ProductCategory>> GetProductCategories(int[] ids);
        Task<ProductCategory> GetProductCategory(int id);


    }
}
