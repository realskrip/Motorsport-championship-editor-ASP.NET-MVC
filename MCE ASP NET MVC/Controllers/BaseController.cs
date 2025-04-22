using MCE_ASP_NET_MVC.Data;
using Microsoft.AspNetCore.Mvc;

namespace MCE_ASP_NET_MVC.Controllers
{
    public abstract class BaseController : Controller
    {
        private protected readonly Microsoft.AspNetCore.Identity.UserManager<Microsoft.AspNetCore.Identity.IdentityUser> userManager;
        private protected readonly ApplicationDbContext db;

        public BaseController(ApplicationDbContext _db, Microsoft.AspNetCore.Identity.UserManager<Microsoft.AspNetCore.Identity.IdentityUser> _userManager)
        {
            db = _db;
            userManager = _userManager;
        }
    }
}
