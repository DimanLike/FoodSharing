using FoodSharing.Models;
using FoodSharing.Models.Users;

namespace FoodSharing.Services.Users.Interfaces
{
    public interface IUserService
    {
        // Users
        Task AddUser(string email, string password);
        Task RegisterUser(RegistrationView registrationView);
        Task<User> GetUserByEmail(string email);

        Task<Guid> GetUserIdByEmail(string email);
        Task<string> GetUserEmailById(Guid userid);
        Task<User> GetUserById(Guid userid);
        Task<List<User>> GetUsers(Guid[] ids);

        // UserProfile
        Task AddUserProfile(UserProfileView model);
        Task SaveUserProfile(UserProfileView model, string email);
        Task<UserProfileView> GetUserProfile(Guid userid);
        Task<UserProfileView> GetUserProfile(string email);

        // ProfileInfo
        Task<ProfileInfoView> GetUserProfileInfo(Guid userid);

        //other
        Task<byte[]> GetAvatar(Guid userid);
    }
}