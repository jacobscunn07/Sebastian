using System;
using System.Collections.Generic;

namespace Sebastian.Api.Domain.Models
{
    public class WorkoutSuperset
    {
        public Guid Id { get; set; }

        public int Sequence { get; set; }

        public Guid WorkoutId { get; set; }
        
        public Workout Workout { get; set; }
        
        public List<WorkoutSupersetExercise> WorkoutSupersetExercises { get; set; }
    }
}
