namespace FoodSharing.Models.Chat
{
    public class MessagesView
    {
        public Guid Id { get; set; }

        public Guid FromUserId { get; set; }
        public string FromUser { get; set; }

        public Guid ToUserId { get; set; }
        public string ToUser { get; set; }

        public string Content { get; set; }

		public string Position { get; set; }

        public DateTime CreatedAt { get; set; }

		

		public MessagesView() { }

		public MessagesView(Guid id, Guid fromUserId, string fromUser, Guid toUserId, string toUser, string content, DateTime createdAt)
		{
			Id = id;
			FromUserId = fromUserId;
			FromUser = fromUser;
			ToUserId = toUserId;
			ToUser = toUser;
			Content = content;
			CreatedAt = createdAt;
		}
	}
}
