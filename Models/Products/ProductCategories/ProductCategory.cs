namespace FoodSharing.Models.Products.ProductCategories
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        //пустой конструктор можно не писать, он создается по умолчанию
    }
}
