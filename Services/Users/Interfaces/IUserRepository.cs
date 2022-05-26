using FoodSharing.Models.Users;

namespace FoodSharing.Services.Users.Interfaces
{
    public interface IUserRepository
    {
        // Users
        Task AddUser(string email, string password);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserById(Guid userid);

        Task<List<User>> GetUsers(Guid[] ids);

        // UserProfile
        Task AddUserProfile(UserProfileView model);
        Task<UserProfile> GetUserProfile(Guid userid);
    }
}
