using FoodSharing.Models;
using FoodSharing.Models.Products.ProductCategories;

namespace FoodSharing.Services.Products.Interfaces
{
    public interface IProductService
    {
        // Products
        Task AddProduct(ProductView model);
        Task DeleteProduct(Guid id);
        Task<List<ProductView>> GetProductsViews(Guid userid);
        Task<ProductView> GetProduct(Guid productid);
        Task EditProduct(ProductView model);

        // ProductCategories
        Task<List<ProductCategory>> GetProductCategories();
        

    }
}
