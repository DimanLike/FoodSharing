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
			Guid userToId = new Guid(userTo);
			string userFromEmail = Context.User.Identity.Name; // отправителя
			string userToEmail = await _userService.GetUserEmailById(userToId); // почта получателя


			Messages messages = new Messages();
			messages.Id = Guid.NewGuid();
			messages.FromUserId = await _userService.GetUserIdByEmail(userFromEmail);
			messages.ToUserId = userToId;
			messages.Content = message;
			messages.CreatedAt = DateTime.Now;

			await _chatService.Send(messages);

			string avatar = Convert.ToBase64String(await _userService.GetAvatar(messages.FromUserId));
			string sender = userFromEmail;

			
			sender = userToEmail;
			await Clients.User(userToEmail).SendAsync("Receive", userFromEmail, messages, avatar, sender);
			sender = userFromEmail;
			await Clients.User(userFromEmail).SendAsync("Receive", userFromEmail, messages, avatar, sender);

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
