using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sebastian.Api.Domain;
using Sebastian.Api.Domain.Models;

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
            _db.BeginTransaction();
            _db.Add(new Workout
            {
                Id = Guid.NewGuid(),
                Name = "Test1",
                DateTimeBegan = DateTime.UtcNow
            });
            _db.CloseTransaction();
            return Task.FromResult(new AddWorkoutResponse());
        }
    }
}