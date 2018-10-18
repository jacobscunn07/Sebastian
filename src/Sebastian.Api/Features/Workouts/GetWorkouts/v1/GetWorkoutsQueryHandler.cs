using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using MediatR;
using Sebastian.Api.Domain;
using Sebastian.Api.Domain.Models;
using Sebastian.Api.Infrastructure;

namespace Sebastian.Api.Features.Workouts.GetWorkouts.v1
{
    public class GetWorkoutsQueryHandler : IRequestHandler<GetWorkoutsQuery, GetWorkoutsResponse>
    {
        private readonly SebastianDbContext _db;
        private readonly IUserPrincipal _userPrincipal;

        public GetWorkoutsQueryHandler(SebastianDbContext db, IUserPrincipal userPrincipal)
        {
            _db = db;
            _userPrincipal = userPrincipal;
        }
        
        public Task<GetWorkoutsResponse> Handle(GetWorkoutsQuery request, CancellationToken cancellationToken)
        {
            if (request.WorkoutId == null || request.WorkoutId == Guid.Empty)
            {
                return GetWorkoutsList(_userPrincipal.User.Id);
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

        private Task<GetWorkoutsResponse> GetWorkoutsList(Guid userId)
        {
            var workouts = _db.Workouts.Where(x => x.UserId == userId).ToList();

            return Task.FromResult(new GetWorkoutsResponse
            {
                Workouts = workouts.Select(x => new GetWorkoutsResponse.Workout
                {
                    Id = x.Id,
                    Name = x.Name,
                    DateTimeBegan = x.DateTimeBegan,
                    DateTimeFinished = x.DateTimeFinished
                })
            });
            
        }
    }
}