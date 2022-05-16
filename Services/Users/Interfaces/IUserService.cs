using FoodSharing.Models;
using FoodSharing.Models.Products;
using FoodSharing.Models.Users;

namespace FoodSharing.Services.Users.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByEmailAndPassword(string email);
        Task AddUserByEmailAndPassword(string email, string password);
        Task AddUserDataProfile(UserProfileViewModel model);
        Task<UserProfileViewModel> GetUserDataProfile(string email);
        Task AddNewUserProduct(ProductsViewModel model);
        Task<List<ProductsViewModel>> GetUserInventory(Guid userid);
        
    }
}