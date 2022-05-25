using FoodSharing.Models;
using FoodSharing.Models.Users;
using FoodSharing.Services.Products.Interfaces;
using FoodSharing.Services.Users.Converters;
using FoodSharing.Services.Users.Interfaces;

namespace FoodSharing.Services.Users
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private IProductService _productService;


		public UserService(IUserRepository userRepository, IProductService productService)
		{
			_userRepository = userRepository;
			_productService = productService;
		}

		public Task AddUser(string email, string password)
		{
			return _userRepository.AddUser(email, password);
		}

		public Task<User> GetUserByEmail(string email)
		{
			return _userRepository.GetUserByEmail(email);
		}

		public Task AddUserProfile(UserProfileViewModel model)
		{
			return _userRepository.AddUserProfile(model);
		}

		public async Task<UserProfileViewModel> GetUserProfile(Guid userid)
		{
			UserProfile userProfile = await _userRepository.GetUserProfile(userid);
			if (userProfile is null) return null;

			return UserConverter.MapToUserProfileView(userProfile);
		}

		public async Task<List<User>> GetUsers(Guid[] ids)
        {
			return await _userRepository.GetUsers(ids);
        }

        public async Task<ProfileInfoView> GetUserProfileInfo(Guid userid)
        {
            ProfileInfoView profileInfoView = new ProfileInfoView();
            UserProfile userProfile = await _userRepository.GetUserProfile(userid);
			List<ProductView> productView = await _productService.GetProductsViews(userid);

			UserProfileViewModel userProfileViewModel = UserConverter.MapToUserProfileView(userProfile);

			profileInfoView.ProductViews = productView;
			profileInfoView.UserProfileView = userProfileViewModel;

			return profileInfoView;
		}
    }
}
