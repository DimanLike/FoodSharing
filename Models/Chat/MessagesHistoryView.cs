namespace FoodSharing.Models.Chat
{
    public class MessagesHistoryView
    {
        public List<MessageView> Messages { get; set; }
        public Guid ToUserId { get; set; }
        public byte[] ToUserAvatar { get; set; }
        public Guid FromUserId { get; set; }
        public byte[] FromUserAvatar { get; set; }
    }
}