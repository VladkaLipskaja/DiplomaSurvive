using System.Collections.Generic;

namespace DiplomaSurvive.Entities
{
    /// <summary>
    /// Event entity class.
    /// </summary>
    public class Event : EventBase
    {
        /// <summary>
        /// Event class constructor.
        /// </summary>
        public Event()
        {
            Leaderboards = new HashSet<Leaderboard>();
        }
        
        /// <summary>
        /// Gets or sets the leaderboards.
        /// </summary>
        /// <value>
        /// The leaderboards.
        /// </value>
        public virtual HashSet<Leaderboard> Leaderboards { get; set; }
    }

    /// <summary>
    /// Event entity base class.
    /// </summary>
    public class EventBase
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int ID { get; set; }
        
        /// <summary>
        /// Gets or sets the reward for the first place.
        /// </summary>
        /// <value>
        /// The reward for the first place.
        /// </value>
        public int Reward1 { get; set; }
        
        /// <summary>
        /// Gets or sets the reward for the second place.
        /// </summary>
        /// <value>
        /// The reward for the second place.
        /// </value>
        public int Reward2 { get; set; }
        
        /// <summary>
        /// Gets or sets the reward for the third place.
        /// </summary>
        /// <value>
        /// The reward for the third place.
        /// </value>
        public int Reward3 { get; set; }
        
        /// <summary>
        /// Gets or sets the time to start.
        /// </summary>
        /// <value>
        /// The time to start.
        /// </value>
        public int Start { get; set; }
        
        /// <summary>
        /// Gets or sets the time to finish.
        /// </summary>
        /// <value>
        /// The time to finish.
        /// </value>
        public int Finish { get; set; }
        
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }
    }
}