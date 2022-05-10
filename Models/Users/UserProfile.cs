namespace FoodSharing.Models.Users
{
    public class UserProfile
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }


        public UserProfile()
        {

        }

        public class UserAvatar
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public IFormFile Image { get; set; }
        }
    }

    
}
