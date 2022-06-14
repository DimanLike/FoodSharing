using Microsoft.AspNetCore.SignalR;

namespace FoodSharing.Tools.SignalR
{
    public class CustomUserIdProvider : IUserIdProvider
    {

        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.Identity.Name;
        }

    }
}
