using FoodSharing.Models.Users;

namespace FoodSharing.Models.Products
{
    public class ProductInfoView
    {
        public UserProfileView UserProfileViewModel { get; set; }
        public ProductView ProductView { get; set; }
        public string UserEmail { get; set; }
        //public ProductInfoView(UserProfileView userProfileViewModel, ProductView productView)
        //{
        //    UserProfileViewModel = userProfileViewModel;
        //    ProductView = productView;
        //}

        public ProductInfoView(UserProfileView userProfileViewModel, ProductView productView, string userEmail)
        {
            UserProfileViewModel = userProfileViewModel;
            ProductView = productView;
            UserEmail = userEmail;
        }
    }
}
