namespace FoodSharing.Models.Chat
{
    public class MessegesHistoryView
    {
        public List<Messages> Messages { get; set; }

        public Guid ToUserId { get; set; }

        public string ToUser { get; set; }

        public Guid FromUserId { get; set; }
        public string FromUser { get; set; }

    }
}
