using System;
using System.Collections.Generic;

namespace Sebastian.Api.Features.Workouts.GetWorkouts.v1
{
    public class GetWorkoutsResponse
    {
        public IList<Workout> Workouts { get; set; }

        public class Workout
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public DateTime DateTimeBegan { get; set; }
            public DateTime? DateTimeFinished { get; set; }
        }
    }
}