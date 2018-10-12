using System;
using MediatR;

namespace Sebastian.Api.Features.Workouts.DeleteWorkout.v1
{
    public class DeleteWorkoutCommand : IRequest
    {
        public Guid WorkoutId { get; set; }
    }
}