using Microsoft.AspNetCore.Mvc;

namespace MCE_ASP_NET_MVC.Controllers
{
    public class NotificationsController : Controller
    {
        public IActionResult ShowNotificationsList()
        {
            return View();
        }
    }
}
