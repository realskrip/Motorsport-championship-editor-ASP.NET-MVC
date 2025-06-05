namespace MCE_ASP_NET_MVC.models
{
    public class GranPrix
    {
        public required string Id { get; set; }
        public required string ChampionshipId { get; set; }
        public required string Name { get; set; }
        public required string Game {  get; set; }
        public required string Discipline { get; set; }
        public required string CarClass { get; set; }
        public required string Track { get; set; }
        public DateTime? Date { get; set; }
        public string? Description { get; set; }
    }
}