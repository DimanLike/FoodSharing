﻿using FoodSharing.Models;
using FoodSharing.Models.Users;

namespace FoodSharing.Services.Users.Interfaces
{
    public interface IUserRepository
    {
        // Users
        Task AddUser(string email, string password);
        Task<User> GetUserByEmailAndPassword(string email);
        Task<List<User>> GetUsers(Guid[] ids);

        // UserProfile
        Task AddUserProfile(UserProfileViewModel model);
        Task<UserProfile> GetUserProfile(string email);
    }
}
