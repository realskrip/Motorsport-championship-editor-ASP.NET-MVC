namespace MCE_ASP_NET_MVC.models
{
    public class GrandPrixResult
    {
        public enum raceStatus
        {
            Finished,
            NotFinished,
            DidNotStart
        }

        public required string GrandPrixId { get; set; }
        public required string ChampionshipMemberId { get; set; }
        public required string Place {  get; set; }
        public required bool BestLap { get; set; }
        public required raceStatus RaceStatus { get; set; }
    }
}