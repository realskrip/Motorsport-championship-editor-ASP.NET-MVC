using MCE_ASP_NET_MVC.Data;
using MCE_ASP_NET_MVC.models;
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

        public async Task<GrandPrixViewModel> ShowGrandPrixsListAsync(ClaimsPrincipal currentUserPrincipal, string championshipId)
        {
            var currentUser = await userManager.GetUserAsync(currentUserPrincipal);

            List<GrandPrix> grandPrixes = db.grandprixes.Where(g => g.ChampionshipId == championshipId).ToList();

            GrandPrixViewModel grandPrixViewModel = new GrandPrixViewModel()
            {
                ChampionshipId = championshipId,
                GrandPrixes = grandPrixes
            };

            return grandPrixViewModel;
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

        public async Task CreateChampionshipAsync(ClaimsPrincipal currentUserPrincipal, string name, string? racingRegulations, string pointsAwardingRules)
        {
            var currentUser = await userManager.GetUserAsync(currentUserPrincipal);

            Championship newChampionship = new Championship()
            {
                Id = Guid.NewGuid().ToString(),
                Name = name.Trim(),
                OwnerId = currentUser.Id,
                RacingRegulations = racingRegulations != null ? racingRegulations.Trim() : string.Empty,
                PointsAwardingRules = pointsAwardingRules.Trim()
            };

            db.championships.Add(newChampionship);
            db.SaveChanges();
        }

        public async Task CreateGrandPrixAsync(ClaimsPrincipal currentUserPrincipal, string championshipId, string name, string game, string discipline, string carClass, string track, DateTime? dateTime, string? description)
        {
            var currentUser = await userManager.GetUserAsync(currentUserPrincipal);

            GrandPrix newGrandPrix = new GrandPrix()
            {
                Id = Guid.NewGuid().ToString(),
                ChampionshipId = championshipId.Trim(),
                Name = name.Trim(),
                Game = game.Trim(),
                Discipline = discipline.Trim(),
                CarClass = carClass.Trim(),
                Track = track.Trim(),
                Date = dateTime,
                Description = description
            };

            db.grandprixes.Add(newGrandPrix);
            db.SaveChanges();
        }
    }
}