using MCE_ASP_NET_MVC.Data;
using MCE_ASP_NET_MVC.models;
using MCE_ASP_NET_MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MCE_ASP_NET_MVC.Services
{
    public class FriendsService : ServiceBase
    {
        public FriendsService(ApplicationDbContext _db, UserManager<IdentityUser> _userManager) : base(_db, _userManager) { }

        public async Task<FriendsViewModel> ShowFriendList(ClaimsPrincipal currentUserPrincipal)
        {
            var currentUser = await userManager.GetUserAsync(currentUserPrincipal);
            FriendsViewModel friendsViewModel = new FriendsViewModel() { currentUserFriendshipСode = currentUser.Id };
            return friendsViewModel;
        }

        public async Task SendFriendRequestAsync(ClaimsPrincipal currentUserPrincipal, string friendshipСode)
        {
            var currentUser = await userManager.GetUserAsync(currentUserPrincipal);
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
                await db.SaveChangesAsync();
            }
        }
    }
}