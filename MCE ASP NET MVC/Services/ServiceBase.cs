using MCE_ASP_NET_MVC.Data;
using Microsoft.AspNetCore.Identity;

namespace MCE_ASP_NET_MVC.Services
{
    public abstract class ServiceBase
    {
        private protected readonly ApplicationDbContext db;
        private protected readonly Microsoft.AspNetCore.Identity.UserManager<Microsoft.AspNetCore.Identity.IdentityUser> userManager;

        public ServiceBase(ApplicationDbContext _db, UserManager<IdentityUser> _userManager)
        {
            db = _db;
            userManager = _userManager;
        }
    }
}
