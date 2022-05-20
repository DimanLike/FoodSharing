using FoodSharing.Models;
using FoodSharing.Models.Users;

namespace FoodSharing.Services.Users.Interfaces
{
    public interface IUserService
    {
        // Users
        Task AddUser(string email, string password);
        Task<User> GetUserByEmailAndPassword(string email);
        Task<List<User>> GetUsers(Guid[] ids);

        // UserProfile
        Task AddUserProfile(UserProfileViewModel model);
        Task<UserProfileViewModel> GetUserProfile(string email);
    }
}