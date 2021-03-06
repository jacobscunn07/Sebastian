﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sebastian.Api.Domain.Models;

namespace Sebastian.Api.Domain.Maps
{
    public class ExerciseTypeMap : IEntityTypeConfiguration<ExerciseType>
    {
        public void Configure(EntityTypeBuilder<ExerciseType> builder)
        {
            builder.ToTable("ExerciseType");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(128).IsRequired();
        }
    }
}
