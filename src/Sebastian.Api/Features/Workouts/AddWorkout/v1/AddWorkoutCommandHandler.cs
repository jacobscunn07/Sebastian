using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sebastian.Api.Domain;
using Sebastian.Api.Domain.Models;
using Sebastian.Api.Infrastructure;

namespace Sebastian.Api.Features.Workouts.AddWorkout.v1
{
    public class AddWorkoutCommandHandler : IRequestHandler<AddWorkoutCommand, AddWorkoutResponse>
    {
        private readonly SebastianDbContext _db;

        public AddWorkoutCommandHandler(SebastianDbContext db)
        {
            _db = db;
        }

        public Task<AddWorkoutResponse> Handle(AddWorkoutCommand request, CancellationToken cancellationToken)
        {   
            if(HasAnInProgressWorkout())
                throw new InvalidSebastianOperationException("There exists a workout in progress already.");
                
            return _db.RunTransaction(() =>
            {
                var workout = new Workout
                {
                    Id = Guid.NewGuid(),
                    UserId = request.UserId,
                    Name = request.Name,
                    DateTimeBegan = DateTime.UtcNow
                };
                _db.Add(workout);
                return new AddWorkoutResponse
                {
                    Id = workout.Id,
                    Name = workout.Name,
                    DateTimeBegan = workout.DateTimeBegan
                };
            });
        }

        private bool HasAnInProgressWorkout()
        {
            return _db.Workouts.Any(x => x.DateTimeFinished == null);
        }
    }
}
