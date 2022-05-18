using FoodSharing.Models;
using FoodSharing.Models.Users;

namespace FoodSharing.Services.Users.Interfaces
{
    public interface IUserRepository
    {
        // Users
        Task AddUser(string email, string password);
        Task<User> GetUserByEmailAndPassword(string email);

        // UserProfile
        Task AddUserProfile(UserProfileViewModel model);
        Task<UserProfile> GetUserProfile(string email);
    }
}
