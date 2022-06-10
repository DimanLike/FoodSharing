

using FoodSharing.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;
using FoodSharing.Services.Users.Interfaces;

namespace FoodSharing.Hubs
{
	[Authorize]
	public class ChatHub : Hub
	{
		private readonly IConfiguration _config;
		private IUserService _userService;

		public ChatHub(IConfiguration config, IUserService userService)
		{
			_config = config;
			_userService = userService;
		}

		public async Task SendMessage(string message, string userTo) 
		{
			Guid UserToId = new Guid(userTo);
			User user = await _userService.GetUserById(UserToId);

			string userFromEmail = Context.User.Identity.Name;
			string userToEmail = user.Email;
			var id = Context.ConnectionId;
			var userid = Context.User.Identity.Name;

			if (userFromEmail != userToEmail)
				await Clients.User(userToEmail).SendAsync("Receive", userFromEmail, message);
			await Clients.User(id).SendAsync("Receive", userFromEmail, message);
			await Clients.All.SendAsync("Receive", user.Email, message);
			await Clients.All.SendAsync("Receive", user, message);    
		}

		public override async Task OnConnectedAsync()
		{
			await Clients.All.SendAsync("Notify", $"{Context.User.Identity.Name} вошёл в чат");
			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
			await Clients.All.SendAsync("Notify", $"{Context.ConnectionId} покинул в чат");
			await base.OnDisconnectedAsync(exception);
		}

	}
}
