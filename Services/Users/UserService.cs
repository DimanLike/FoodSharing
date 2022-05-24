using FoodSharing.Models;
using FoodSharing.Models.Users;
using FoodSharing.Services.Users.Converters;
using FoodSharing.Services.Users.Interfaces;

namespace FoodSharing.Services.Users
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
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
	}
}
