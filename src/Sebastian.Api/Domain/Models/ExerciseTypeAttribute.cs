using System;
using System.Collections.Generic;

namespace Sebastian.Api.Domain.Models
{
    public class ExerciseTypeAttribute
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<ExerciseTypeExerciseTypeAttribute> ExerciseTypeExerciseTypeAttributes { get; set; }
    }
}
