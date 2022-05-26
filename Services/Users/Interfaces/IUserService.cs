using FoodSharing.Models;
using FoodSharing.Models.Users;

namespace FoodSharing.Services.Users.Interfaces
{
    public interface IUserService
    {
        // Users
        Task AddUser(string email, string password);
        Task<User> GetUserByEmail(string email);
        Task<List<User>> GetUsers(Guid[] ids);

        // UserProfile
        Task AddUserProfile(UserProfileView model);
        Task<UserProfileView> GetUserProfile(Guid userid);

        // ProfileInfo
        Task<ProfileInfoView> GetUserProfileInfo(Guid userid);
    }
}