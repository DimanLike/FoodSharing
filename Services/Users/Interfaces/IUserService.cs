using FoodSharing.Models.Users;

namespace FoodSharing.Services.Users.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByEmailAndPassword(string email, string password);
        Task AddUserByEmailAndPassword(string email, string password);
    }
}