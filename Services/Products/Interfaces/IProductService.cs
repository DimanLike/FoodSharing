using FoodSharing.Models;
using FoodSharing.Models.Products;
using FoodSharing.Models.Products.ProductCategories;

namespace FoodSharing.Services.Products.Interfaces
{
    public interface IProductService
    {
        // Products
        Task SaveProduct(ProductView model);
        Task DeleteProduct(Guid id);
        Task<List<ProductView>> GetProductsViews(Guid userid);
        Task<List<ProductView>> GetProductsViews(Guid userid, Guid currentUserId);
        Task<ProductView> GetProduct(Guid productid);
        Task EditProduct(ProductView model);

        // ProductCategories
        Task<List<ProductCategory>> GetProductCategories();

        //Catalog

        Task<List<ProductView>> GetCatalogViews(int categoryId, Guid currentUserId);

        //Favourites
        Task ChangeProductFavourite(Guid userid, Guid productid);
        Task<List<ProductView>> GetProductsFavouritesViews(Guid userid);

    }
}
