using MCE_ASP_NET_MVC.Data;
using MCE_ASP_NET_MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MCE_ASP_NET_MVC.Services
{
    public class NotificationsService : ServiceBase
    {
        public NotificationsService(ApplicationDbContext _db, UserManager<IdentityUser> _userManager) : base(_db, _userManager) { }

        public async Task<NotificationsViewModel> ShowNotificationsListAsync(ClaimsPrincipal currentUserPrincipal)
        {
            var currentUser = await userManager.GetUserAsync(currentUserPrincipal);
            var notifications = db.notifications.Where(n => n.userId == currentUser.Id);
            NotificationsViewModel notificationsViewModel = new NotificationsViewModel() { notifications = notifications };

            return notificationsViewModel;
        }

        public void RejectNotification(string notificationId)
        {
            var removedNotification = db.notifications.Find(notificationId);
            db.notifications.Remove(removedNotification);
            db.SaveChanges();
        }
    }
}
