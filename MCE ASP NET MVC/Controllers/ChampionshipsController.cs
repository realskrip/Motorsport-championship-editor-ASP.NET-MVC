using MCE_ASP_NET_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace MCE_ASP_NET_MVC.Controllers
{
    public class ChampionshipsController : Controller
    {
        private readonly ChampionshipsService championshipService;
        public ChampionshipsController(ChampionshipsService _championshipService)
        {
            championshipService = _championshipService;
        }

        public async Task<IActionResult> ShowChampionshipsListAsync()
        {
            return View(await championshipService.ShowChampionshipsListAsync(User));
        }

        [HttpPost]
        public IActionResult RemoveChampionship(string id)
        {
            championshipService.RemoveChampionship(id);
            return RedirectToAction("ShowChampionshipsList");
        }

        public IActionResult ShowCreateChampionshipForm()
        {
            return View("CreateChampionshipForm");
        }

        [HttpPost]
        public async Task<IActionResult> CreateChampionshipAsync(string name, string? racingRegulations, string pointsAwardingRules)
        {
            await championshipService.CreateChampionshipAsync(User, name, racingRegulations, pointsAwardingRules);
            return RedirectToAction("ShowChampionshipsList");
        }
    }
}