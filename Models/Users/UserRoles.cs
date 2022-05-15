namespace FoodSharing.Models.Users
{
    public class UserRoles
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public UserRoles (){}
    }
}
