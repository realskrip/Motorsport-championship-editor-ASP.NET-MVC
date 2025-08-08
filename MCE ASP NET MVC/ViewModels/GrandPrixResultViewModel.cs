using MCE_ASP_NET_MVC.models;

namespace MCE_ASP_NET_MVC.ViewModels
{
    public class GrandPrixResultViewModel
    {
        public List<string>? grandPrixMemberName { get; set; }
        public List<GrandPrixResult>? grandPrixResults {  get; set; }
    }
}