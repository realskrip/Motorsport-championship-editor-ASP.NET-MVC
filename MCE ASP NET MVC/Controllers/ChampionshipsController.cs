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

        public async Task<IActionResult> ShowChampionshipAsync(string championshipId)
        {
            return View(await championshipService.ShowChampionshipAsync(User, championshipId));
        }

        [HttpPost]
        public IActionResult RemoveChampionship(string id)
        {
            championshipService.RemoveChampionship(id);
            return RedirectToAction("ShowChampionshipsList");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveGrandPrixAsync(string championshipId, string grandPrixId)
        {
            championshipService.RemoveGrandPrix(grandPrixId);
            return View("ShowChampionship", await championshipService.ShowChampionshipAsync(User, championshipId));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveMemberAsync(string championshipId, string memberId)
        {
            championshipService.RemoveChampionshipMember(championshipId, memberId);
            return View("ShowChampionship", await championshipService.ShowChampionshipAsync(User, championshipId));
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
            return View("ShowChampionship", await championshipService.ShowChampionshipAsync(User, championshipId));
        }

        [HttpPost]
        public async Task<IActionResult> SubmitRequestJoinChampionshipAsync(string championshipId)
        {
            await championshipService.SubmitRequestJoinChampionshipAsync(User, championshipId);
            return RedirectToAction("ShowChampionshipsList");
        }

        [HttpPost]
        public async Task<IActionResult> AddChampionshipMemberAsync(string championshipId, string newMemberId, string notificationId)
        {
            await championshipService.AddChampionshipMemberAsync(User, championshipId, newMemberId, notificationId);
            return RedirectToAction("ShowNotificationsList", "Notifications");
        }

        [HttpPost]
        public IActionResult LeaveChampionship(string userId, string championshipId)
        {
            championshipService.LeaveChampionship(userId, championshipId);
            return RedirectToAction("ShowChampionshipsList");
        }
    }
}