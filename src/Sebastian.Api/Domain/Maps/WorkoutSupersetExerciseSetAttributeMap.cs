using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sebastian.Api.Domain.Models;

namespace Sebastian.Api.Domain.Maps
{
    public class WorkoutSupersetExerciseSetAttributeMap : IEntityTypeConfiguration<WorkoutSupersetExerciseSetAttribute>
    {
        public void Configure(EntityTypeBuilder<WorkoutSupersetExerciseSetAttribute> builder)
        {
            builder.ToTable("WorkoutSupersetExerciseSetAttribute");
            
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(128).IsRequired();

            builder.Property(x => x.Value).HasMaxLength(128);

            builder
                .HasOne(x => x.WorkoutSupersetExerciseSet)
                .WithMany(x => x.WorkoutSupersetExerciseSetAttributes)
                .HasForeignKey(x => x.WorkoutSupersetExerciseSetId);
        }
    }
}
