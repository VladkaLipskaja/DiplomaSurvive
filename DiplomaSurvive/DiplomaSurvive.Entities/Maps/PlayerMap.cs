using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiplomaSurvive.Entities
{
    /// <summary>
    /// Player map class.
    /// </summary>
    public class PlayerMap : IEntityTypeConfiguration<Player>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            // Primary key
            builder.Property(t => t.ID).ValueGeneratedOnAdd();

            // Properties
            builder.ToTable("players");

            builder.Property(t => t.ID).HasColumnName("id");
            builder.Property(t => t.UserName).HasColumnName("username");
            builder.Property(t => t.Scores).HasColumnName("scores");
            builder.Property(t => t.LeaderboardID).HasColumnName("leaderboardid");
            builder.Property(t => t.Reward).HasColumnName("reward");
            
            builder.HasOne(t => t.Leaderboard).WithMany(t => t.Players).HasForeignKey(t => t.LeaderboardID);
        }
    }
}