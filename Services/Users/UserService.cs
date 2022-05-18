﻿using FoodSharing.Models;
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

		public Task<User> GetUserByEmailAndPassword(string email)
		{
			return _userRepository.GetUserByEmailAndPassword(email);
		}

		public Task AddUserProfile(UserProfileViewModel model)
		{
			return _userRepository.AddUserProfile(model);
		}

		public async Task<UserProfileViewModel> GetUserProfile(string email)
		{
			UserProfile userProfile = await _userRepository.GetUserProfile(email);
			if (userProfile is null) return null;

			return UserConverter.MapToUserProfileView(userProfile);
		}
	}
}
