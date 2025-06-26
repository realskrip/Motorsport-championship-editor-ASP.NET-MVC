using MCE_ASP_NET_MVC.models;

namespace MCE_ASP_NET_MVC.ViewModels
{
    public class ChampionshipsListViewModel
    {
        public required string CurrentUserId { get; set; }
        public IEnumerable<Championship>? Championships { get; set; }
    }
}