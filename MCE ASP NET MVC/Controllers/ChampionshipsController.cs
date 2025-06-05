using MCE_ASP_NET_MVC.models;
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

        public async Task<IActionResult> ShowGrandPrixsListAsync(string championshipId)
        {
            return View(await championshipService.ShowGrandPrixsListAsync(User, championshipId));
        }

        [HttpPost]
        public IActionResult RemoveChampionship(string id)
        {
            championshipService.RemoveChampionship(id);
            return RedirectToAction("ShowChampionshipsList");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveGrandPrix(string championshipId, string grandPrixId)
        {
            championshipService.RemoveGrandPrix(grandPrixId);
            return View("ShowGrandPrixsList", await championshipService.ShowGrandPrixsListAsync(User, championshipId));
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

        public IActionResult ShowCreateGrandPrixForm(string championshipId)
        {
            ViewBag.championshipId = championshipId;
            return View("CreateGrandPrixForm");
        }

        [HttpPost]
        public async Task<IActionResult> CreateGrandPrixAsync(string championshipId, string name, string game, string discipline, string carClass, string track, DateTime? dateTime, string? description)
        {
            await championshipService.CreateGrandPrixAsync(User, championshipId, name, game, discipline, carClass, track, dateTime, description);
            return View("ShowGrandPrixsList", await championshipService.ShowGrandPrixsListAsync(User, championshipId));
        }
    }
}