namespace FoodSharing.Models.Chat
{
    public class Messages
    {
        public Guid Id { get; set; }

        public Guid FromUserId { get; set; }

        public Guid ToUserId { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; }

    }
}
