namespace DiplomaSurvive.Api
{
    public class GetEventResponse
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int ID { get; set; }

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