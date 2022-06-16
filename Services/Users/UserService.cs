using FoodSharing.Models;
using FoodSharing.Models.Users;
using FoodSharing.Services.Products.Interfaces;
using FoodSharing.Services.Users.Converters;
using FoodSharing.Services.Users.Interfaces;
using FoodSharing.Tools;

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
		
		public async Task RegisterUser(RegistrationView registrationView)
		{
			await AddUser(registrationView.Email, registrationView.Password);

			UserProfileView userprofile = new UserProfileView();
			userprofile.Id = Guid.NewGuid();
			userprofile.Email = registrationView.Email;

			await AddUserProfile(userprofile);
		}

		public Task<User> GetUserByEmail(string email)
		{
			return _userRepository.GetUserByEmail(email);
		}

		public async Task<Guid> GetUserIdByEmail(string email)
        {
			User user = await _userRepository.GetUserByEmail(email);
			return user.Id;
        }

		public async Task<string> GetUserEmailById(Guid userid)
		{
			User user = await _userRepository.GetUserById(userid);
			return user.Email;
		}

		public Task<User> GetUserById(Guid userid)
        {
			return _userRepository.GetUserById(userid);
        }

		public Task AddUserProfile(UserProfileView model)
		{
			return _userRepository.SaveUserProfile(model);
		}

		public async Task SaveUserProfile(UserProfileView model, string email)
		{
			UserProfileView userProfile = await GetUserProfile(email);

			model.Id = userProfile.Id;

			if (model.Image != null)
			{
				model.Avatar = FileTools.GetBytes(model.Image);
			}
			else
			{
				model.Avatar = userProfile.Avatar;
			}

			await _userRepository.SaveUserProfile(model);
		}

		public async Task<UserProfileView> GetUserProfile(Guid userid)
		{
			UserProfile userProfile = await _userRepository.GetUserProfile(userid);
			if (userProfile is null) return null;

			return UserConverter.MapToUserProfileView(userProfile);
		}

		public async Task<UserProfileView> GetUserProfile(string email)
		{
			User user = await GetUserByEmail(email);
			UserProfile userProfile = await _userRepository.GetUserProfile(user.Id);

			if (userProfile is null) return null;

			return UserConverter.MapToUserProfileView(userProfile);
		}

		public async Task<byte[]> GetAvatar(Guid userid)
		{
			UserProfile userProfile = await _userRepository.GetUserProfile(userid);
			if (userProfile is null) return null;
			return userProfile.Avatar;
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

			UserProfileView userProfileViewModel = UserConverter.MapToUserProfileView(userProfile);

			profileInfoView.ProductViews = productView;
			profileInfoView.UserProfileView = userProfileViewModel;

			return profileInfoView;
		}


    }
}
