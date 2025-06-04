namespace MCE_ASP_NET_MVC.models
{
    public class Championship
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public string? RacingRegulations { get; set; }
        public required string OwnerId { get; set; }
        public required string PointsAwardingRules { get; set; }
    }
}