namespace FoodSharing.Models.Users
{
    public class UserProfile
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public byte[] Avatar { get; set; }


        public UserProfile()
        {

        }

     
    }

    
}
