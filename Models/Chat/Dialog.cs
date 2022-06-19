namespace FoodSharing.Models.Chat
{
    public class Dialog
    {
        public Guid Id { get; set; }
        public Guid FromUserId { get; set; }
        public string FromUserName { get; set; }
        public byte[] FromUserAvatar { get; set; }
        public Guid ToUserId { get; set; }
        public string ToUserName { get; set; }
        public byte[] ToUserAvatar { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }


        public Dialog() { }

        public Dialog(Guid id, Guid fromUserId, string fromUserName, byte[] fromUserAvatar, Guid toUserId, string toUserName, byte[] toUserAvatar, string content, DateTime createdAt)
        {
            Id = id;
            FromUserId = fromUserId;
            FromUserName = fromUserName;
            FromUserAvatar = fromUserAvatar;
            ToUserId = toUserId;
            ToUserName = toUserName;
            ToUserAvatar = toUserAvatar;
            Content = content;
            CreatedAt = createdAt;
        }
    }
}
