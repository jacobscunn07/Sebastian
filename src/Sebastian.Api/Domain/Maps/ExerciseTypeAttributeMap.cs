using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sebastian.Api.Domain.Models;

namespace Sebastian.Api.Domain.Maps
{
    public class ExerciseTypeAttributeMap : IEntityTypeConfiguration<ExerciseTypeAttribute>
    {
        public void Configure(EntityTypeBuilder<ExerciseTypeAttribute> builder)
        {
            builder.ToTable("ExerciseTypeAttribute");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(128).IsRequired();
        }
    }
}
