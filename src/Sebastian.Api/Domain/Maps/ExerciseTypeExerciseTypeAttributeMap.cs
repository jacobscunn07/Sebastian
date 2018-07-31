using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sebastian.Api.Domain.Models;

namespace Sebastian.Api.Domain.Maps
{
    public class ExerciseTypeExerciseTypeAttributeMap : IEntityTypeConfiguration<ExerciseTypeExerciseTypeAttribute>
    {
        public void Configure(EntityTypeBuilder<ExerciseTypeExerciseTypeAttribute> builder)
        {
            builder.ToTable("ExerciseType_ExerciseTypeAttribute");

            builder.HasKey(x => x.Id);

            builder
                .HasOne(x => x.ExerciseType)
                .WithMany(x => x.ExerciseTypeExerciseTypeAttributes)
                .HasForeignKey(x => x.ExerciseTypeId);

            builder
                .HasOne(x => x.ExerciseTypeAttribute)
                .WithMany(x => x.ExerciseTypeExerciseTypeAttributes)
                .HasForeignKey(x => x.ExerciseTypeAttributeId);
        }
    }
}
