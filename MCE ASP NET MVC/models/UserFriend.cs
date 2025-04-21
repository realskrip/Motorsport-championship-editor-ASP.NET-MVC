using System.ComponentModel.DataAnnotations;

namespace MCE_ASP_NET_MVC.Models
{
    public class UserFriend
    {
        public required string userId { get; set; }
        public required string friendId { get; set; }
    }
}