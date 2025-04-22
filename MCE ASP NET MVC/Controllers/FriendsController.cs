using MCE_ASP_NET_MVC.Data;
using MCE_ASP_NET_MVC.models;
using MCE_ASP_NET_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MCE_ASP_NET_MVC.Controllers
{
    public class FriendsController : BaseController
    {
        public FriendsController(ApplicationDbContext db, Microsoft.AspNetCore.Identity.UserManager<Microsoft.AspNetCore.Identity.IdentityUser> userManager) : base(db, userManager) { }

        public async Task<IActionResult> ShowFriendList()
        {
            var currentUser = await userManager.GetUserAsync(User);

            FriendsViewModel friendsViewModel = new FriendsViewModel() { currentUserFriendshipСode = currentUser.Id };

            return View(friendsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddFriendRequest(string friendshipСode)
        {
            var currentUser = await userManager.GetUserAsync(User);
            friendshipСode = friendshipСode.ToLower().Trim();

            if (friendshipСode != currentUser.Id)
            {
                Notification newNotification = new Notification()
                { 
                    id = Guid.NewGuid().ToString(),
                    userId = friendshipСode,
                    notification = string.Format("{0} wants to add you as a friend. Accept?", currentUser.UserName),
                    type = Notification.NotificationType.FriendRequest,
                    newFriendId = currentUser.Id,
                    newFriendName = currentUser.UserName 
                };
                
                db.notifications.Add(newNotification);
                db.SaveChanges();
            }

            return RedirectToAction("ShowFriendList");
        }
    }
}