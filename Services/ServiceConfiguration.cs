using FoodSharing.Services.Chat;
using FoodSharing.Services.Chat.Interfaces;
using FoodSharing.Services.Chat.Repositories;
using FoodSharing.Services.Products;
using FoodSharing.Services.Products.Interfaces;
using FoodSharing.Services.Products.Repositories;
using FoodSharing.Services.Users;
using FoodSharing.Services.Users.Interfaces;
using FoodSharing.Services.Users.Repositories;
using FoodSharing.Tools.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace FoodSharing.Services
{
    public static class ServiceConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            //Services
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IChatService, ChatService>();

            //Repositoryes
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IChatRepository, ChatRepository>();

            services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();


        }
    }
}
