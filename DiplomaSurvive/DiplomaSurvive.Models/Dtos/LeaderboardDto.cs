namespace DiplomaSurvive.Models
{
    public class LeaderboardDto
    {
        public int Place { get; set; }
        
        public LeaderboardPlayer[] Players { get; set; }

        public class LeaderboardPlayer
        {
            public int Place { get; set; }
            
            public string Name { get; set; }
            
            public int Score { get; set; }
        }
    }
}