namespace FoodSharing.Models.Products.ProductFavorites
{
    public class Favourites
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }


    }
}
