using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sebastian.Api.Domain.Models;

namespace Sebastian.Api.Domain.Maps
{
    public class ExerciseMap : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.ToTable("Exercise");
            
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Name).HasMaxLength(128).IsRequired();

            builder
                .HasOne(x => x.ExerciseType)
                .WithOne(x => x.Exercise)
                .HasForeignKey<ExerciseType>(x => x.ExerciseId)
                .HasForeignKey<Exercise>(x => x.ExerciseTypeId);
        }
    }
}
