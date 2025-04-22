using MCE_ASP_NET_MVC.Data;
using MCE_ASP_NET_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MCE_ASP_NET_MVC.Controllers
{
    public class NotificationsController : BaseController
    {
        public NotificationsController(ApplicationDbContext db, Microsoft.AspNetCore.Identity.UserManager<Microsoft.AspNetCore.Identity.IdentityUser> userManager) : base(db, userManager) { }
        public async Task<IActionResult> ShowNotificationsList()
        {
            var currentUser = await userManager.GetUserAsync(User);
            var notifications = db.notifications.Where(n => n.userId == currentUser.Id);
            NotificationsViewModel notificationsViewModel = new NotificationsViewModel() { notifications = notifications };

            return View(notificationsViewModel);
        }

        [HttpPost]
        public IActionResult RejectNotification(string notificationId)
        {
            var removedNotification = db.notifications.Find(notificationId);
            db.notifications.Remove(removedNotification);
            db.SaveChanges();

            return RedirectToAction("ShowNotificationsList");
        }
    }
}