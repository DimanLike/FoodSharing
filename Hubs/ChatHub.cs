using FoodSharing.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;
using FoodSharing.Services.Users.Interfaces;
using FoodSharing.Services.Chat.Interfaces;
using FoodSharing.Models.Chat;

namespace FoodSharing.Hubs
{
	[Authorize]
	public class ChatHub : Hub
	{
		private readonly IConfiguration _config;
		private IUserService _userService;
		private IChatService _chatService;

		public ChatHub(IConfiguration config, IUserService userService, IChatService chatService)
		{
			_config = config;
			_userService = userService;
			_chatService = chatService;
		}

		public async Task SendMessage(string message, string userTo) 
		{
			string userFromEmail = Context.User.Identity.Name; // отправителя
			string userToEmail = userTo; // почта получателя

			
			Messages messages = new Messages();
			messages.Id = Guid.NewGuid();
			messages.FromUserId = await _userService.GetUserIdByEmail(userFromEmail);
			messages.ToUserId = await _userService.GetUserIdByEmail(userTo);
			messages.Content = message;
			messages.CreatedAt = DateTime.Now;

			await _chatService.Send(messages);

			if (userFromEmail != userToEmail)
				await Clients.User(userToEmail).SendAsync("Receive", userFromEmail, message);
			await Clients.User(userFromEmail).SendAsync("Receive", userFromEmail, message);

			//await Clients.Client(id).SendAsync("Receive", userFromEmail, message);
		}

		public override async Task OnConnectedAsync()
		{
			await Clients.All.SendAsync("Notify", $"{Context.User.Identity.Name} вошёл в чат");
			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
			await Clients.All.SendAsync("Notify", $"{Context.User.Identity.Name} покинул в чат");
			await base.OnDisconnectedAsync(exception);
		}

	}
}
