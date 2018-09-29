using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sebastian.Api.Domain;
using Sebastian.Api.Domain.Models;
using Sebastian.Api.Infrastructure;

namespace Sebastian.Api.Features.Workouts.GetWorkouts.v1
{
    public class GetWorkoutsQueryHandler : IRequestHandler<GetWorkoutsQuery, GetWorkoutsResponse>
    {
        private readonly SebastianDbContext _db;

        public GetWorkoutsQueryHandler(SebastianDbContext db)
        {
            _db = db;
        }
        
        public Task<GetWorkoutsResponse> Handle(GetWorkoutsQuery request, CancellationToken cancellationToken)
        {
            if (request.WorkoutId == null || request.WorkoutId == Guid.Empty)
            {
                return GetWorkoutsList();
            }

            return GetWorkout(request.WorkoutId);
        }

        private Task<GetWorkoutsResponse> GetWorkout(Guid? requestWorkoutId)
        {
            try
            {
                var workout = _db.Find<Workout>(requestWorkoutId);
                return Task.FromResult(new GetWorkoutsResponse
                {
                    Workouts = new List<GetWorkoutsResponse.Workout>
                    {
                        new GetWorkoutsResponse.Workout
                        {
                            Id = workout.Id,
                            Name = workout.Name,
                            DateTimeBegan = workout.DateTimeBegan,
                            DateTimeFinished = workout.DateTimeFinished
                        }
                    }
                });
            }
            catch (Exception e)
            {
                throw new InvalidSebastianOperationException($"Unable to find a workout with id {requestWorkoutId}");
            }
            
        }

        private Task<GetWorkoutsResponse> GetWorkoutsList()
        {
            throw new NotImplementedException();
        }
    }
}