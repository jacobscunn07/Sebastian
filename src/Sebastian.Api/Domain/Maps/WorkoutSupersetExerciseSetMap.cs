using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sebastian.Api.Domain.Models;

namespace Sebastian.Api.Domain.Maps
{
    public class WorkoutSupersetExerciseSetMap : IEntityTypeConfiguration<WorkoutSupersetExerciseSet>
    {
        public void Configure(EntityTypeBuilder<WorkoutSupersetExerciseSet> builder)
        {
            builder.ToTable("WorkoutSupersetExerciseSet");
            
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Sequence).IsRequired();

            builder
                .HasOne(x => x.WorkoutSupersetExercise)
                .WithMany(x => x.WorkoutSupersetExerciseSets)
                .HasForeignKey(x => x.WorkoutSupersetExerciseId);
        }
    }
}
