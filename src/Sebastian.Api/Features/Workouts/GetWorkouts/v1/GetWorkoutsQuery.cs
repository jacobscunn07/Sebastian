using System;
using MediatR;

namespace Sebastian.Api.Features.Workouts.GetWorkouts.v1
{
    public class GetWorkoutsQuery : IRequest<GetWorkoutsResponse>
    {
        public Guid UserId { get; set; }
        public Guid? WorkoutId { get; set; }
    }
}