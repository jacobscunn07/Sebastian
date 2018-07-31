using System;
using System.Collections.Generic;

namespace Sebastian.Api.Domain.Models
{
    public class WorkoutSupersetExerciseSet
    {
        public Guid Id { get; set; }

        public int Sequence { get; set; }
        
        public Guid WorkoutSupersetExerciseId { get; set; }

        public WorkoutSupersetExercise WorkoutSupersetExercise { get; set; }

        public List<WorkoutSupersetExerciseSetAttribute> WorkoutSupersetExerciseSetAttributes { get; set; }
    }
}
