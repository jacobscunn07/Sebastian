using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sebastian.Api.Domain.Models;

namespace Sebastian.Api.Domain.Maps
{
    public class WorkoutSupersetMap : IEntityTypeConfiguration<WorkoutSuperset>
    {
        public void Configure(EntityTypeBuilder<WorkoutSuperset> builder)
        {
            builder.ToTable("WorkoutSuperset");
            
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Sequence).IsRequired();
            
            builder
                .HasOne(x => x.Workout)
                .WithMany(x => x.WorkoutSupersets)
                .HasForeignKey(x => x.WorkoutId);
        }
    }
}
