using MCE_ASP_NET_MVC.Data;
using MCE_ASP_NET_MVC.models;
using MCE_ASP_NET_MVC.Models;
using MCE_ASP_NET_MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MCE_ASP_NET_MVC.Services
{
    public class ChampionshipsService : ServiceBase
    {
        public ChampionshipsService(ApplicationDbContext _db, UserManager<IdentityUser> _userManager)
    : base(_db, _userManager) { }

        public async Task<ChampionshipsViewModel> ShowChampionshipsListAsync(ClaimsPrincipal currentUserPrincipal)
        {
            var currentUser = await userManager.GetUserAsync(currentUserPrincipal);

            List<Championship> championships = db.championships.Where(c => c.OwnerId == currentUser.Id).ToList();

            ChampionshipsViewModel championshipsViewModel = new ChampionshipsViewModel() { Championships = championships };

            return championshipsViewModel;
        }

        public void RemoveChampionship(string id)
        {
            Championship? championship = db.championships.Where(c => c.Id == id).FirstOrDefault();

            if (championship != null)
            {
                db.championships.Remove(championship);
                db.SaveChanges();
            }
        }
    }
}