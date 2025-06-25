namespace MCE_ASP_NET_MVC.models
{
    public class ChampionshipMember
    {
        public enum rightType
        {
            Reading,
            FullAccess
        }

        public required string ChampionshipId { get; set; }
        public required string UserId { get; set; }
        public required rightType RightType { get; set; }
    }
}