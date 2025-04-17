using Microsoft.AspNetCore.Mvc;

namespace MCE_ASP_NET_MVC.Controllers
{
    public class FriendsController : Controller
    {
        public IActionResult ShowFriendList()
        {
            return View("Friends");
        }
    }
}