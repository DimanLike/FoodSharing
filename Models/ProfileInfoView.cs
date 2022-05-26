using FoodSharing.Models.Users;

namespace FoodSharing.Models
{
    public class ProfileInfoView
    {
        public UserProfileView UserProfileView { get; set; }

        public List<ProductView> ProductViews { get; set; }
    }
}
