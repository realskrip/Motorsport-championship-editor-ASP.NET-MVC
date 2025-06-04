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
    }
}