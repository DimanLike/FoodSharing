using FoodSharing.Models.Products.ProductCategories;

namespace FoodSharing.Models.Products
{
    public class CatalogListView
    {
        public int CategoryId { get; set; }
        public List<ProductView> CatalogViews { get; set; }

        public List<ProductCategory> ProductCategories { get; set; }
    }
}
