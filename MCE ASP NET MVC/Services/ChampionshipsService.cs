using MCE_ASP_NET_MVC.Data;
using MCE_ASP_NET_MVC.models;
using MCE_ASP_NET_MVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using NuGet.Versioning;
using System.Security.Claims;
using static MCE_ASP_NET_MVC.models.ChampionshipMember;

namespace MCE_ASP_NET_MVC.Services
{
    public class ChampionshipsService : ServiceBase
    {
        private readonly NotificationsService notificationsService;
        public ChampionshipsService(ApplicationDbContext _db, UserManager<IdentityUser> _userManager, NotificationsService _notificationsService) : base(_db, _userManager)
        {
            notificationsService = _notificationsService;
        }

        internal async Task<ChampionshipsListViewModel> ShowChampionshipsListAsync(ClaimsPrincipal currentUserPrincipal)
        {
            var currentUser = await userManager.GetUserAsync(currentUserPrincipal);
            Championship championship;
            List<ChampionshipMember> participationInChampionships = db.championship_members.Where(c => c.UserId == currentUser.Id).ToList();
            List<Championship> championships = db.championships.Where(c => c.OwnerId == currentUser.Id).ToList();

            if (participationInChampionships.Count() > 0)
            {
                foreach (var item in participationInChampionships)
                {
                    championship = db.championships.Where(c => c.Id == item.ChampionshipId && c.OwnerId != currentUser.Id).FirstOrDefault();

                    if (championship != null)
                    {
                        championships.Add(championship);
                        championship = null;
                    }
                }
            }

            ChampionshipsListViewModel championshipsViewModel = new ChampionshipsListViewModel()
            {
                CurrentUserId = currentUser.Id,
                Championships = championships
            };

            return championshipsViewModel;
        }

        internal async Task<ChampionshipViewModel> ShowChampionshipAsync(ClaimsPrincipal currentUserPrincipal, string championshipId)
        {
            var currentUser = await userManager.GetUserAsync(currentUserPrincipal);

            rightType currentUserChampionshipRight = rightType.Reading;
            Championship championship = db.championships.Where(c => c.Id == championshipId).FirstOrDefault();
            List<GrandPrix> grandPrixes = db.grandprixes.Where(g => g.ChampionshipId == championshipId).ToList();
            List<ChampionshipMember> championshipMembers = db.championship_members.Where(m => m.ChampionshipId == championshipId).ToList();

            if (championship.OwnerId == currentUser.Id)
                currentUserChampionshipRight = rightType.FullAccess;


            ChampionshipViewModel championshipViewModel = new ChampionshipViewModel()
            {
                ChampionshipId = championshipId,
                GrandPrixes = grandPrixes,
                CurrentUserChampionshipRight = currentUserChampionshipRight,
                IndividualChampionshipStandings = FormationIndividualChampionshipStandings(championshipId)
            };

            if (championshipMembers != null)
            {
                championshipViewModel.ChampionshipMembers = new List<ChampionshipViewModel.ChampionshipMemberViewStruct>();

                foreach (var item in championshipMembers)
                {
                    ChampionshipViewModel.ChampionshipMemberViewStruct member = new ChampionshipViewModel.ChampionshipMemberViewStruct()
                    {
                        memberId = item.UserId,
                        userName = db.Users.Where(u => u.Id == item.UserId).FirstOrDefault().UserName
                    };

                    championshipViewModel.ChampionshipMembers.Add(member);
                }
            }

            return championshipViewModel;
        }

        internal void RemoveChampionship(string id)
        {
            List<GrandPrix> grandPrixes = db.grandprixes.Where(gp => gp.ChampionshipId == id).ToList();
            List<ChampionshipMember> championshipMembers = db.championship_members.Where(c => c.ChampionshipId == id).ToList();
            Championship? championship = db.championships.Where(c => c.Id == id).FirstOrDefault();

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (grandPrixes.Count() > 0)
                        foreach (var item in grandPrixes)
                            db.grandprixes.Remove(item);

                    if (championshipMembers.Count() > 0)
                        foreach (var item in championshipMembers)
                            db.championship_members.Remove(item);

                    if (championship != null)
                        db.championships.Remove(championship);

                    db.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

        internal void RemoveGrandPrix(string grandPrixId)
        {
            GrandPrix? grandPrix = db.grandprixes.Where(gp => gp.Id == grandPrixId).FirstOrDefault();

            if (grandPrix != null)
            {
                db.grandprixes.Remove(grandPrix);
                db.SaveChanges();
            }
        }

        internal void RemoveChampionshipMember(string championshipId, string memberId)
        {
            ChampionshipMember championshipMember = db.championship_members.Where(m => m.ChampionshipId == championshipId && m.UserId == memberId).FirstOrDefault();

            db.championship_members.Remove(championshipMember);
            db.SaveChanges();
        }

        internal async Task CreateChampionshipAsync(ClaimsPrincipal currentUserPrincipal, string name, string? racingRegulations, string pointsAwardingRules)
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

        internal async Task CreateGrandPrixAsync(ClaimsPrincipal currentUserPrincipal, string championshipId, string name, string game, string discipline, string carClass, string track, DateTime? dateTime, string? description)
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

            FormationGrandPrixResultsTable(newGrandPrix.Id, championshipId);
        }

        internal async Task SubmitRequestJoinChampionshipAsync(ClaimsPrincipal currentUserPrincipal, string championshipId)
        {
            championshipId = championshipId.Trim();

            var currentUser = await userManager.GetUserAsync(currentUserPrincipal);
            var championship = db.championships.Where(c => c.Id == championshipId).FirstOrDefault();
            bool isParticipant = db.championship_members.Where(m => m.ChampionshipId == championshipId && m.UserId == currentUser.Id).Any();
            bool isRepeatSubmit = db.notifications.Where(n => n.userId == championship.OwnerId && n.newMemberId == currentUser.Id && n.championshipId == championshipId).Any();

            if (championship == null)
                return;

            if (isParticipant == true)
                return;

            if (isRepeatSubmit == true)
                return;

            Notification newNotification = new Notification()
            {
                id = Guid.NewGuid().ToString(),
                userId = championship.OwnerId,
                notification = string.Format("{0} wants to join the championship {1}. Accept?", currentUser.UserName, championship.Name),
                type = Notification.NotificationType.СhampionshipRequest,
                newMemberId = currentUser.Id,
                championshipId = championship.Id,
            };

            db.notifications.Add(newNotification);
            db.SaveChanges();
        }

        internal async Task AddChampionshipMemberAsync(ClaimsPrincipal currentUserPrincipal, string championshipId, string newMemberId, string notificationId)
        {
            var currentUser = await userManager.GetUserAsync(currentUserPrincipal);
            Championship championship = db.championships.Where(c => c.Id == championshipId).FirstOrDefault();

            if (championship != null)
            {
                ChampionshipMember championshipMember = new ChampionshipMember()
                {
                    ChampionshipId = championshipId,
                    UserId = newMemberId,
                    RightType = newMemberId == currentUser.Id ? ChampionshipMember.rightType.FullAccess : ChampionshipMember.rightType.Reading
                }
            ;

                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.championship_members.Add(championshipMember);
                        notificationsService.RejectNotification(notificationId);
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }

                var grandPrixes = db.grandprixes.Where(gp => gp.ChampionshipId == championshipId).ToList();
                if (grandPrixes != null && grandPrixes.Count() > 0)
                {
                    foreach (var item in grandPrixes)
                    {
                        FormationGrandPrixResultsTable(item.Id, championshipId);
                    }
                }
            }
        }

        internal void LeaveChampionship(string userId, string championshipId)
        {
            ChampionshipMember championshipMember = db.championship_members.Where(m => m.UserId == userId && m.ChampionshipId == championshipId).FirstOrDefault();

            if (championshipMember != null)
            {
                db.championship_members.Remove(championshipMember);
                db.SaveChanges();
            }
        }

        internal async Task<GrandPrixResultViewModel> ShowGrandPrixResultAsync(ClaimsPrincipal currentUserPrincipal, string grandPrixId, string championshipId)
        {
            var currentUser = await userManager.GetUserAsync(currentUserPrincipal);

            rightType currentUserChampionshipRight = rightType.Reading;
            Dictionary<string, string> memberNames = new Dictionary<string, string>();
            Championship championship = db.championships.Where(c => c.Id == championshipId).FirstOrDefault();

            if (championship.OwnerId == currentUser.Id)
                currentUserChampionshipRight = rightType.FullAccess;

            var result = db.grandprix_results.Where(r => r.GrandPrixId == grandPrixId).ToList();

            if (result != null)
            {
                foreach (var item in result)
                {
                    var member = db.Users.Where(u => u.Id == item.ChampionshipMemberId).FirstOrDefault();

                    memberNames.Add(member.Id, member.UserName);
                }
            }

            GrandPrixResultViewModel grandPrixResult = new GrandPrixResultViewModel()
            {
                grandPrixId = grandPrixId,
                championshipId = championshipId,
                grandPrixResults = result,
                grandPrixMemberName = memberNames,
                CurrentUserChampionshipRight = currentUserChampionshipRight
            };

            return grandPrixResult;
        }

        private void FormationGrandPrixResultsTable(string grandPrixId, string championshipId)
        {
            List<ChampionshipMember> championshipMemberList = db.championship_members.Where(m => m.ChampionshipId == championshipId).ToList();

            if (championshipMemberList.Count() <= 0)
                return;

            foreach (var item in championshipMemberList)
            {
                var result = db.grandprix_results.Where(r => r.GrandPrixId == grandPrixId && r.ChampionshipMemberId == item.UserId).FirstOrDefault();

                if (result == null)
                {
                    GrandPrixResult grandPrixResult = new GrandPrixResult()
                    {
                        GrandPrixId = grandPrixId,
                        ChampionshipMemberId = item.UserId,
                        Place = "-",
                        BestLap = false,
                        RaceStatus = GrandPrixResult.raceStatus.DidNotStart,
                        Points = "-"
                    };

                    db.grandprix_results.Add(grandPrixResult);
                    db.SaveChanges();
                }
            }
        }

        internal void GrandPrixResultsUpdate(int bestLapIndex, List<GrandPrixResult> grandPrixResults)
        {
            for (int i = 0; i < grandPrixResults.Count; i++)
            {
                if (i == bestLapIndex)
                    grandPrixResults[i].BestLap = true;
                else
                    grandPrixResults[i].BestLap = false;

                db.grandprix_results.Update(grandPrixResults[i]);
            }

            db.SaveChanges();
        }

        internal string[,] FormationIndividualChampionshipStandings(string championshipId)
        {
            var grandPrixes = db.grandprixes.Where(gp => gp.ChampionshipId == championshipId).ToList();
            var championshipMembers = db.championship_members.Where(m => m.ChampionshipId == championshipId).ToList();

            int rows = championshipMembers.Count() + 1;
            int columns = grandPrixes.Count() + 2;

            string[,] resultTable = new string[rows, columns];
            resultTable[0, 0] = "Racer";
            resultTable[0, 1] = "Σ";

            var grandPrixResults = (
                from gpr in db.grandprix_results
                join gp in db.grandprixes on gpr.GrandPrixId equals gp.Id
                where gp.ChampionshipId == championshipId
                select gpr
                ).ToList();

            // Set grandPrixes id
            for (int i = 2, j = 0; i < columns; i++, j++)
            {
                resultTable[0, i] = grandPrixes[j].Id;
            }

            // Set championship members names
            for (int i = 1, j = 0; i < rows; i++, j++)
            {
                resultTable[i, 0] = championshipMembers[j].UserId;
            }

            // Set points
            for (int i = 1; i < rows; i++)
            {
                for (int j = 2; j < columns; j++)
                {
                    resultTable[i, j] = grandPrixResults.Where(p => p.ChampionshipMemberId == resultTable[i, 0] && p.GrandPrixId == resultTable[0, j]).FirstOrDefault().Points;
                }
            }

            // Set points sum
            for (int i = 1; i < rows; i++)
            {
                int sum = 0;

                foreach (var item in grandPrixResults)
                {
                    if (item.ChampionshipMemberId == resultTable[i, 0])
                    {
                        if (Int32.TryParse(item.Points, out int x) == true)
                            sum += x;
                    }
                }

                resultTable[i, 1] = sum.ToString();
            }

            // Replace championship member Id with championship member name
            for (int i = 1; i < rows; i++)
                resultTable[i, 0] += db.Users.Where(u => u.Id == resultTable[i, 0]).FirstOrDefault().UserName;

            // Replace grand prix Id with grand prix name
            for (int i = 2; i < columns; i++)
                resultTable[0, i] += grandPrixes.Where(gp => gp.Id == resultTable[0, i]).FirstOrDefault().Name;

            return resultTable;
        }
    }
}