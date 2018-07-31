using System;

namespace Sebastian.Api.Domain.Models
{
    public class WorkoutSupersetExerciseSetAttribute
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
        
        public Guid WorkoutSupersetExerciseSetId { get; set; }

        public WorkoutSupersetExerciseSet WorkoutSupersetExerciseSet { get; set; }
    }
}
