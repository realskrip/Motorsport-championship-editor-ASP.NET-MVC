using MCE_ASP_NET_MVC.models;

namespace MCE_ASP_NET_MVC.ViewModels
{
    public class ChampionshipViewModel
    {
        public struct ChampionshipMemberViewStruct
        {
            public string userId;
            public string userName;
        }

        public required string ChampionshipId { get; set; }
        public IEnumerable<GrandPrix>? GrandPrixes { get; set; }
        public List<ChampionshipMemberViewStruct>? ChampionshipMembers { get; set; }
    }
}