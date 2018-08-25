using System;
using MediatR;

namespace Sebastian.Api.Features.Workouts.AddWorkout.v1
{
    public class AddWorkoutCommand : IRequest<AddWorkoutResponse>
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
    }
}