namespace MCE_ASP_NET_MVC.models
{
    public class Notification
    {
        public enum NotificationType
        {
            FriendRequest,
            championshipRequest
        }

        public required string id { set; get; }
        public required string userId { set; get; }
        public required string notification { set; get; }
        public required NotificationType type { set; get; }
        public string? newFriendId { set; get; }
        public string? newFriendName { set; get; }
    }
}