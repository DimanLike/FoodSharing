using FoodSharing.Models;
using FoodSharing.Models.Products;
using FoodSharing.Models.Products.ProductCategories;
using FoodSharing.Models.Products.ProductFavorites;

namespace FoodSharing.Services.Products.Interfaces
{
    public interface IProductRepository
    {
        // Products
        Task AddProduct(ProductView model);
        Task DeleteProduct(Guid id);
        Task<List<Product>> GetProducts(Guid userid);
        Task<List<Product>> GetSelectionProducts(Guid[] userids);
        Task<Product> GetProduct(Guid id);
        Task EditProduct(ProductView model);

        // ProductCategories
        Task<List<ProductCategory>> GetProductCategories();
        Task<List<ProductCategory>> GetProductCategories(int[] ids);
        Task<ProductCategory> GetProductCategory(int id);

        // Catalog
        Task<List<Product>> GetCatalog();
        Task<List<Product>> GetCatalog(int categoryId);

        // ProductFavorites
        Task AddProductFavourites(Favourite model);
        Task DeleteProductFavourites(Guid id);
        Task<Favourite> GetProductFavourites(Guid userid, Guid productid);
        Task<List<Guid>> GetUserProductFavouritesIds(Guid userid);
        Task<List<Favourite>> GetProductFavourites(Guid[] userids, Guid[] productids);
        Task<List<Favourite>> GetUserProductFavourites(Guid userid);

    }
}
