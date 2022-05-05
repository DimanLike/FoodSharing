using FoodSharing.Models.Users;

namespace FoodSharing.Services.Users.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAndPassword(string email);
        Task AddUserByEmailAndPassword(string email, string password);
        Task AddUserDataProfile(Guid id, string firstname, string lastname, string email, string adress, string phone);
    }
}
