using MCE_ASP_NET_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace MCE_ASP_NET_MVC.Controllers
{
    public class NotificationsController : Controller
    {
        private readonly NotificationsService notificationsService;
        public NotificationsController(NotificationsService _notificationsService)
        {
            notificationsService = _notificationsService;
        }

        public async Task<IActionResult> ShowNotificationsListAsync()
        {
            return View("ShowNotificationsList", await notificationsService.ShowNotificationsListAsync(User));
        }

        [HttpPost]
        public IActionResult RejectNotification(string notificationId)
        {
            notificationsService.RejectNotification(notificationId);
            return RedirectToAction("ShowNotificationsListAsync");
        }
    }
}