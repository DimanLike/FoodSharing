namespace FoodSharing.Models.Users
{
    public class ProductCategories
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public ProductCategories() { }
    }
}
