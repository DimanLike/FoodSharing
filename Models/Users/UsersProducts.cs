namespace FoodSharing.Models.Products
{
    public class UsersProducts
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public  string Category { get; set; }
        public string Quantity { get; set; }
        public byte[] Image { get; set; }
        public DateTime CreatedAt { get; set; }

        public UsersProducts(){}


    }

    //public class Category
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}
