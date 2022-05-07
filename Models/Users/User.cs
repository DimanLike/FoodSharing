namespace FoodSharing.Models.Users
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }

        public User()
        {

        }

        public User(Guid id, string email, string password)
        {
            Id = id;
            Email = email;
            Password = password;
        }
    }
}
