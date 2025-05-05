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

        public async Task<IActionResult> ShowFriendListAsync()
        {
            return View(await friendService.ShowFriendListAsync(User));
        }

        [HttpPost]
        public async Task<IActionResult> SendFriendRequestAsync(string friendshipСode)
        {
            await friendService.SendFriendRequestAsync(User, friendshipСode);
            return RedirectToAction("ShowFriendList");
        }

        [HttpPost]
        public async Task<IActionResult> AddFriendAsync(string notificationId, string newFriendId)
        {
            await friendService.AddFriendAsync(User, notificationId, newFriendId);
            return RedirectToAction("ShowNotificationsList", "Notifications");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFriend(string friendId)
        {
            await friendService.RemoveFriend(User, friendId);
            return RedirectToAction("ShowFriendList");
        }
    }
}