using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sebastian.Api.Domain.Models;

namespace Sebastian.Api.Domain.Maps
{
    public class WorkoutMap : IEntityTypeConfiguration<Workout>
    {
        public void Configure(EntityTypeBuilder<Workout> builder)
        {
            builder.ToTable("Workout");
            
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Name).HasMaxLength(128).IsRequired();
            
            builder.Property(x => x.DateTimeBegan).IsRequired();
        }
    }
}
