namespace DiplomaSurvive.Entities
{
    /// <summary>
    /// Player entity class.
    /// </summary>
    public class Player : PlayerBase
    {
        /// <summary>
        /// Gets or sets the leaderboard.
        /// </summary>
        /// <value>
        /// The leaderboard
        /// </value>
        public virtual Leaderboard Leaderboard { get; set; }
    }

    /// <summary>
    /// Player entity base class.
    /// </summary>
    public class PlayerBase
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int ID { get; set; }
        
        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        /// <value>
        /// The user name.
        /// </value>
        public string UserName { get; set; }
        
        /// <summary>
        /// Gets or sets the number of scores.
        /// </summary>
        /// <value>
        /// The number of scores.
        /// </value>
        public int Scores { get; set; }
        
        /// <summary>
        /// Gets or sets the leaderboard identifier.
        /// </summary>
        /// <value>
        /// The leaderboard identifier.
        /// </value>
        public int LeaderboardID { get; set; }
        
        /// <summary>
        /// Gets or sets the reward.
        /// </summary>
        /// <value>
        /// The reward.
        /// </value>
        public int Reward { get; set; }
    }
}