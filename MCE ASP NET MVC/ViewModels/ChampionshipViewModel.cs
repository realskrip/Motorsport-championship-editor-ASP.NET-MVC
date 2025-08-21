using MCE_ASP_NET_MVC.models;
using static MCE_ASP_NET_MVC.models.ChampionshipMember;

namespace MCE_ASP_NET_MVC.ViewModels
{
    public class ChampionshipViewModel
    {
        public struct ChampionshipMemberViewStruct
        {
            public string memberId;
            public string userName;
        }

        public required string ChampionshipId { get; set; }
        public IEnumerable<GrandPrix>? GrandPrixes { get; set; }
        public List<ChampionshipMemberViewStruct>? ChampionshipMembers { get; set; }
        public required rightType CurrentUserChampionshipRight {  get; set; }
        public string[,]? IndividualChampionshipStandings { get; set; }
    }
}