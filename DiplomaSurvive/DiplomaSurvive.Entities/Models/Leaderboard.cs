using System.Collections.Generic;

namespace DiplomaSurvive.Entities
{
    /// <summary>
    /// Leaderboard entity class.
    /// </summary>
    public class Leaderboard : LeaderboardBase
    {
        /// <summary>
        /// Leaderboard class constructor.
        /// </summary>
        public Leaderboard()
        {
            Players = new HashSet<Player>();
        }
        
        /// <summary>
        /// Gets or sets the event.
        /// </summary>
        /// <value>
        /// The event.
        /// </value>
        public virtual Event Event { get; set; }
        
        /// <summary>
        /// Gets or sets the players.
        /// </summary>
        /// <value>
        /// The players.
        /// </value>
        public virtual HashSet<Player> Players { get; set; }
    }

    /// <summary>
    /// Leaderboard entity base class.
    /// </summary>
    public class LeaderboardBase
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the number of places.
        /// </summary>
        /// <value>
        /// The number of places.
        /// </value>
        public int Places { get; set; }

        /// <summary>
        /// Gets or sets the number of reserved places.
        /// </summary>
        /// <value>
        /// The number of reserved places.
        /// </value>
        public int ReservedPlaces { get; set; }

        /// <summary>
        /// Gets or sets the event identifier.
        /// </summary>
        /// <value>
        /// The event identifier.
        /// </value>
        public int EventID { get; set; }
    }
}