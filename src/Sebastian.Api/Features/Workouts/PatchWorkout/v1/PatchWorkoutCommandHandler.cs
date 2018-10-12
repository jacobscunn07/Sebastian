using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sebastian.Api.Domain;

namespace Sebastian.Api.Features.Workouts.PatchWorkout.v1
{
    public class PatchWorkoutCommandHandler : AsyncRequestHandler<PatchWorkoutCommand>
    {
        private readonly SebastianDbContext _db;

        public PatchWorkoutCommandHandler(SebastianDbContext db)
        {
            _db = db;
        }

        protected override Task Handle(PatchWorkoutCommand request, CancellationToken cancellationToken)
        {
            _db.RunTransaction(() =>
            {
                var workout = _db.Workouts.Find(request.WorkoutId);
                workout.Name = request.Name;
            });
            
            return Task.CompletedTask;
        }
    }
}