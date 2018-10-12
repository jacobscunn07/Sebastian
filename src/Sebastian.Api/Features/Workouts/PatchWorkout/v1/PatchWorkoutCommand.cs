using System;
using MediatR;

namespace Sebastian.Api.Features.Workouts.PatchWorkout.v1
{
    public class PatchWorkoutCommand : IRequest
    {
        public Guid WorkoutId { get; set; }
        public string Name { get; set; }
    }
}