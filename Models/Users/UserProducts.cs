namespace FoodSharing.Models.Products
{
    public class UserProducts
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public  int CategoryId { get; set; }
        public string Quantity { get; set; }
        public byte[] Image { get; set; }
        public DateTime CreatedAt { get; set; }

     

        public UserProducts(){}
    }
}