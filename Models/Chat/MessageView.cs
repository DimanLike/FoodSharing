namespace FoodSharing.Models.Chat
{
    public class MessageView
    {
        public Guid Id { get; set; }
        public Guid FromUserId { get; set; }
        public string FromUserName { get; set; }
        public Guid ToUserId { get; set; }
        public string ToUserName { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

		public MessageView() { }

		public MessageView(Guid id, Guid fromUserId, string fromUser, Guid toUserId, string toUser, string content, DateTime createdAt)
		{
			Id = id;
			FromUserId = fromUserId;
			FromUserName = fromUser;
			ToUserId = toUserId;
			ToUserName = toUser;
			Content = content;
			CreatedAt = createdAt;
		}
	}
}
