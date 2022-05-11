namespace FoodSharing.Models.Users
{
    public class UserAvatar
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile Image { get; set; }

    }
}
