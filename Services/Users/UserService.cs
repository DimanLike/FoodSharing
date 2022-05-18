using AutoMapper;
using FoodSharing.Models;
using FoodSharing.Models.Products;
using FoodSharing.Models.Users;
using FoodSharing.Services.Users.Converters;
using FoodSharing.Services.Users.Interfaces;
using System.Linq;

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

		public Task AddUserDataProfile(UserProfileViewModel model)
		{
			return _userRepository.AddUserDataProfile(model);
		}
		public async Task<UserProfileViewModel> GetUserDataProfile(string email)
		{
			UserProfile userProfile = await _userRepository.GetUserDataProfile(email);
			if (userProfile is null) return null;

			return UserConverter.MapToUserProfileView(userProfile);
		}
		public Task AddNewUserProduct(ProductsViewModel model)
		{
			return _userRepository.AddNewUserProduct(model);
		}

		public Task DeleteProduct(Guid id)
        {
			return _userRepository.DeleteProduct(id);
        }

		public async Task<List<ProductsViewModel>> GetUserInventory(Guid userid)
		{
			List<UserProducts> userProducts = await _userRepository.GetUserInventory(userid);
			
			if (userProducts is null) { return null; }

			int [] categoryIds = userProducts.Select(x => x.CategoryId).ToArray();
			List<ProductCategories> productCategories = await _userRepository.GetCategories(categoryIds);

			return userProducts.Select(x =>
			{
				ProductCategories category = productCategories.FirstOrDefault(c => c.Id == x.CategoryId);
				return new ProductsViewModel(x.Id, 
											x.UserId, 
											x.Name, 
											x.Description, 
											x.CategoryId, 
											category.Name, 
											x.Quantity, 
											x.Image, 
											x.CreatedAt );
			}).ToList();
			//var configuration = new MapperConfiguration(cfg =>
			//{
			//    cfg.CreateMap<UserProducts, ProductsViewModel>();
			//});
			//var mapper = configuration.CreateMapper();

			//var productsViewModel = mapper.Map<List<UserProducts>, List<ProductsViewModel>> (userProducts);


		}

		public async Task<List<ProductCategories>> GetCategories()
		{
			List<ProductCategories> productCategories = await _userRepository.GetCategories();
			return productCategories;
		}


	}
}
