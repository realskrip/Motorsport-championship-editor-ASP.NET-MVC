using MCE_ASP_NET_MVC.Data;
using Microsoft.AspNetCore.Mvc;

namespace MCE_ASP_NET_MVC.Controllers
{
    public abstract class BaseController : Controller
    {
        private protected readonly Microsoft.AspNetCore.Identity.UserManager<Microsoft.AspNetCore.Identity.IdentityUser> _userManager;
        private protected readonly ApplicationDbContext _db;

        public BaseController(ApplicationDbContext db, Microsoft.AspNetCore.Identity.UserManager<Microsoft.AspNetCore.Identity.IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
    }
}
