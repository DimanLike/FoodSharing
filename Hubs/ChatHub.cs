﻿

using FoodSharing.Models.Users;
using Microsoft.AspNetCore.SignalR;

namespace FoodSharing.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            await Clients.All.SendAsync("broadcastMessage", name, message);
        }

    }
}
