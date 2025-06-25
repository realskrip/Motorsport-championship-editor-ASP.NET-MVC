using Azure.Core;
using MCE_ASP_NET_MVC.Data;
using MCE_ASP_NET_MVC.models;
using MCE_ASP_NET_MVC.Models;
using MCE_ASP_NET_MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MCE_ASP_NET_MVC.Services
{
    public class FriendsService : ServiceBase
    {
        private readonly NotificationsService notificationsService;

        public FriendsService(ApplicationDbContext _db, UserManager<IdentityUser> _userManager, NotificationsService _notificationsService) : base(_db, _userManager)
        {
            notificationsService = _notificationsService;
        }

        internal async Task<FriendsViewModel> ShowFriendListAsync(ClaimsPrincipal currentUserPrincipal)
        {
            var currentUser = await userManager.GetUserAsync(currentUserPrincipal);
            List<UserFriend> userFriends = await db.user_friends.Where(f => f.userId == currentUser.Id).ToListAsync();

            FriendsViewModel friendsViewModel = new FriendsViewModel()
            {
                currentUserFriendshipСode = currentUser.Id,
                friends = new List<Friend>()
            };

            if (userFriends != null && userFriends.Any() == true)
            {
                foreach (var item in userFriends)
                {
                    Friend friend = new Friend();
                    friend.friendId = item.friendId;
                    friend.friendName = db.Users.Where(u => u.Id == item.friendId).FirstOrDefault().UserName;
                    friendsViewModel.friends.Add(friend);
                }
            }

            return friendsViewModel;
        }

        internal async Task SendFriendRequestAsync(ClaimsPrincipal currentUserPrincipal, string friendshipСode)
        {
            friendshipСode = friendshipСode.ToLower().Trim();

            var currentUser = await userManager.GetUserAsync(currentUserPrincipal);
            bool isRepeatRequest = db.notifications.Where(n => n.userId == friendshipСode && n.newFriendId == currentUser.Id).Any();
            bool isFriend = db.user_friends.Where(f => f.userId == currentUser.Id && f.friendId == friendshipСode).Any();

            if (friendshipСode != currentUser.Id && isRepeatRequest == false && isFriend == false)
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
        }

        internal async Task AddFriendAsync(ClaimsPrincipal currentUserPrincipal, string notificationId, string newFriendId)
        {
            var currentUser = await userManager.GetUserAsync(currentUserPrincipal);

            if (db.user_friends.Any(f => f.userId == currentUser.Id && f.friendId == newFriendId) == false)
            {
                UserFriend newFriend = new UserFriend()
                {
                    userId = currentUser.Id,
                    friendId = newFriendId
                };

                UserFriend mutualAddition = new UserFriend()
                {
                    userId = newFriendId,
                    friendId = currentUser.Id
                };

                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.user_friends.Add(newFriend);
                        db.user_friends.Add(mutualAddition);
                        notificationsService.RejectNotification(notificationId);
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        internal async Task RemoveFriend(ClaimsPrincipal currentUserPrincipal, string friendId)
        {
            var currentUser = await userManager.GetUserAsync(currentUserPrincipal);

            using (var transaction = db.Database.BeginTransaction())
            {
                UserFriend newFriend = new UserFriend()
                {
                    userId = currentUser.Id,
                    friendId = friendId
                };

                UserFriend mutualRemoved = new UserFriend()
                {
                    userId = friendId,
                    friendId = currentUser.Id
                };

                try
                {
                    db.user_friends.Remove(newFriend);
                    db.user_friends.Remove(mutualRemoved);
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }
    }
}