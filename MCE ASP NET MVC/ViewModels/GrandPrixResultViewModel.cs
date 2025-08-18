using MCE_ASP_NET_MVC.models;

namespace MCE_ASP_NET_MVC.ViewModels
{
    public class GrandPrixResultViewModel
    {
        public required string grandPrixId { get; set; }
        public required string championshipId { get; set; }
        public List<string>? grandPrixMemberName { get; set; }
        public List<GrandPrixResult>? grandPrixResults {  get; set; }
    }
}