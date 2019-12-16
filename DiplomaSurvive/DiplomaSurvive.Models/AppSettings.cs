namespace DiplomaSurvive.Models
{
    public class AppSettings
    {
        /// <summary>
        /// Gets or sets the secret.
        /// </summary>
        /// <value>
        /// The secret.
        /// </value>
        public string Secret { get; set; }

        /// <summary>
        /// Gets or sets the salt.
        /// </summary>
        /// <value>
        /// The salt.
        /// </value>
        public string Salt { get; set; }
    }
}