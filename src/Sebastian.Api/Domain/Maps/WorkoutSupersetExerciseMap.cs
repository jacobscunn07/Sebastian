using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sebastian.Api.Domain.Models;

namespace Sebastian.Api.Domain.Maps
{
    public class WorkoutSupersetExerciseMap : IEntityTypeConfiguration<WorkoutSupersetExercise>
    {
        public void Configure(EntityTypeBuilder<WorkoutSupersetExercise> builder)
        {
            builder.ToTable("WorkoutSupersetExercise");
            
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Sequence).IsRequired();
            
            builder
                .HasOne(x => x.WorkoutSuperset)
                .WithMany(x => x.WorkoutSupersetExercises)
                .HasForeignKey(x => x.WorkoutSupersetId);
        }
    }
}
