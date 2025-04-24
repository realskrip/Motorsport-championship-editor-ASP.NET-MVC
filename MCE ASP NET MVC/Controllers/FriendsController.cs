using MCE_ASP_NET_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace MCE_ASP_NET_MVC.Controllers
{
    public class FriendsController : Controller
    {
        private readonly FriendsService friendService;
        public FriendsController(FriendsService _friendService)
        {
            friendService = _friendService;
        }

        public async Task<IActionResult> ShowFriendList()
        {
            return View(await friendService.ShowFriendList(User));
        }

        [HttpPost]
        public async Task<IActionResult> SendFriendRequest(string friendshipСode)
        {
            await friendService.SendFriendRequestAsync(User, friendshipСode);
            return RedirectToAction("ShowFriendList");
        }
    }
}