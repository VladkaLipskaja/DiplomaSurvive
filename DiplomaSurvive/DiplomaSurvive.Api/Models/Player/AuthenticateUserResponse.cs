namespace DiplomaSurvive.Api
{
    public class AuthenticateUserResponse
    {
        public int Scores { get; set; }
        
        public int Reward { get; set; }
        
        public string Token { get; set; }
    }
}