using FoodSharing.Models;
using FoodSharing.Models.Users;

namespace FoodSharing.Services.Users.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAndPassword(string email);
        Task AddUserByEmailAndPassword(string email, string password);
        Task AddUserDataProfile(UserProfileViewModel model);
        Task<UserProfile> GetUserDataProfile(string email);
    }
}
