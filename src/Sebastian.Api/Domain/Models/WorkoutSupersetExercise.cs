using System;
using System.Collections.Generic;

namespace Sebastian.Api.Domain.Models
{
    public class WorkoutSupersetExercise
    {
        public Guid Id { get; set; }

        public int Sequence { get; set; }
        
        public Guid WorkoutSupersetId { get; set; }

        public WorkoutSuperset WorkoutSuperset { get; set; }
        
        public Guid ExerciseId { get; set; }

        public Exercise Exercise { get; set; }

        public List<WorkoutSupersetExerciseSet> WorkoutSupersetExerciseSets { get; set; }
    }
}
