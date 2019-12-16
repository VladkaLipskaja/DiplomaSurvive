using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiplomaSurvive.Entities
{
    /// <summary>
    /// Event map class.
    /// </summary>
    public class EventMap : IEntityTypeConfiguration<Event>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            // Primary key
            builder.Property(t => t.ID).ValueGeneratedOnAdd();

            // Properties
            builder.ToTable("events");

            builder.Property(t => t.ID).HasColumnName("id");
            builder.Property(t => t.Reward1).HasColumnName("reward1");
            builder.Property(t => t.Reward2).HasColumnName("reward2");
            builder.Property(t => t.Reward3).HasColumnName("reward3");
            builder.Property(t => t.Start).HasColumnName("start");
            builder.Property(t => t.Finish).HasColumnName("finish");
            builder.Property(t => t.Title).HasColumnName("title");
        }
    }
}