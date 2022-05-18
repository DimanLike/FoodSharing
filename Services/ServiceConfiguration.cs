using FoodSharing.Services.Products;
using FoodSharing.Services.Products.Interfaces;
using FoodSharing.Services.Products.Repositories;
using FoodSharing.Services.Users;
using FoodSharing.Services.Users.Interfaces;
using FoodSharing.Services.Users.Repositories;

namespace FoodSharing.Services
{
    public static class ServiceConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            // синглтоны располагай по алфавиту
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IUserService, UserService>();

            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
        }
    }
}
