using MCE_ASP_NET_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MCE_ASP_NET_MVC.Controllers
{
    public class FriendsController : Controller
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<Microsoft.AspNetCore.Identity.IdentityUser> _userManager;

        public FriendsController(Microsoft.AspNetCore.Identity.UserManager<Microsoft.AspNetCore.Identity.IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult ShowFriendList()
        {
            Task<Microsoft.AspNetCore.Identity.IdentityUser?> currentUser = _userManager.GetUserAsync(User);

            FriendsViewModel friendsViewModel = new FriendsViewModel() { currentUserFriendshipСode = currentUser.Id.ToString() };

            return View(friendsViewModel);
        }
    }
}