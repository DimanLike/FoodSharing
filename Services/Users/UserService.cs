using FoodSharing.Models.Users;
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

        public Task<User> GetUserByEmailAndPassword(string email)
        {
            return _userRepository.GetUserByEmailAndPassword(email);
        }

        public Task AddUserByEmailAndPassword(string email, string password)
        {
            return _userRepository.AddUserByEmailAndPassword(email, password);
        }

        public Task AddUserDataProfile(Guid id, string firstname, string lastname, string email, string adress, string phone)
        {
            return _userRepository.AddUserDataProfile(id, firstname, lastname, email, adress, phone);
        }
    }
}
