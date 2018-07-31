using System;
using System.Collections.Generic;

namespace Sebastian.Api.Domain.Models
{
    public class ExerciseType
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid ExerciseId { get; set; }

        public Exercise Exercise { get; set; }

        public List<ExerciseTypeExerciseTypeAttribute> ExerciseTypeExerciseTypeAttributes { get; set; }
    }
}
