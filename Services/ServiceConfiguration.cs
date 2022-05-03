using FoodSharing.Services.Users;
using FoodSharing.Services.Users.Interfaces;
using FoodSharing.Services.Users.Repositories;

namespace FoodSharing.Services
{
    public static class ServiceConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IUserService, UserService>();

            services.AddSingleton<IUserRepository, UserRepository>();
        }
    }
}
