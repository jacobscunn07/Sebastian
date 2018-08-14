using System;

namespace Sebastian.Api.Features.Workouts.AddWorkout.v1
{
    public class AddWorkoutResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateTimeBegan { get; set; }
    }
}