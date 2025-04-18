using MCE_ASP_NET_MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MCE_ASP_NET_MVC.Controllers
{
    public class FriendsController : Controller
    {
        public IActionResult ShowFriendList()
        {
            FriendsViewModel friendsViewModel = new FriendsViewModel() { UserId = "123" };

            return View(friendsViewModel);
        }
    }
}