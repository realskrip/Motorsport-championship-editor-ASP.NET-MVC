using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCE_ASP_NET_MVC.Models
{
    public class UserFriend
    {
        [Key, Column(Order = 0)]
        public int userId { get; set; }
        
        [Key, Column(Order = 1)]
        public int friendId { get; set; }
    }
}