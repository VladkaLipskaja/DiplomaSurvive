using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiplomaSurvive.Entities
{
    /// <summary>
    /// Leaderboard map class.
    /// </summary>
    public class LeaderboardMap : IEntityTypeConfiguration<Leaderboard>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<Leaderboard> builder)
        {
            // Properties
            builder.ToTable("leaderboards");

            builder.Property(t => t.ID).HasColumnName("id");
            builder.Property(t => t.Places).HasColumnName("places");
            builder.Property(t => t.ReservedPlaces).HasColumnName("reservedplaces");
            builder.Property(t => t.EventID).HasColumnName("eventid");
            
            builder.HasOne(t => t.Event).WithMany(t => t.Leaderboards).HasForeignKey(t => t.EventID);
        }
    }
}