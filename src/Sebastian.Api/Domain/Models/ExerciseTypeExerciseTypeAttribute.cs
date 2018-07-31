using System;

namespace Sebastian.Api.Domain.Models
{
    public class ExerciseTypeExerciseTypeAttribute
    {
        public Guid Id { get; set; }

        public Guid ExerciseTypeId { get; set; }

        public ExerciseType ExerciseType { get; set; }

        public Guid ExerciseTypeAttributeId { get; set; }

        public ExerciseTypeAttribute ExerciseTypeAttribute { get; set; }
    }
}
