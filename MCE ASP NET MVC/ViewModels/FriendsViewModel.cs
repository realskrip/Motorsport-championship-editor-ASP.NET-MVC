using MCE_ASP_NET_MVC.Models;

namespace MCE_ASP_NET_MVC.ViewModels
{
    public struct Friend
    {
        public string friendId;
        public string friendName;
    }

    public class FriendsViewModel
    {
        public required string currentUserFriendshipСode { get; set; }
        public List<Friend>? friends { get; set; }
    }
}