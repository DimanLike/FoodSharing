﻿using FoodSharing.Models;
using FoodSharing.Models.Products;
using FoodSharing.Models.Users;

namespace FoodSharing.Services.Users.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAndPassword(string email);
        Task AddUserByEmailAndPassword(string email, string password);
        Task AddUserDataProfile(UserProfileViewModel model);
        Task<UserProfile> GetUserDataProfile(string email);
        Task AddNewUserProduct(ProductsViewModel model);
        Task<List<UserProducts>> GetUserInventory(Guid userid);
        Task<List<ProductCategories>> GetCategories();

        Task<List<ProductCategories>> GetCategories(int[] ids);
        Task DeleteProduct(Guid id);

    }
}
