namespace MCE_ASP_NET_MVC.models
{
    public class Notification
    {
        public required long id { set; get; }
        public required string userId { set; get; }
        public required string notification { set; get; }
    }
}