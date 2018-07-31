using System;

namespace Sebastian.Api.Domain.Models
{
    public class Exercise
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid ExerciseTypeId { get; set; }

        public ExerciseType ExerciseType { get; set; }
    }
}
