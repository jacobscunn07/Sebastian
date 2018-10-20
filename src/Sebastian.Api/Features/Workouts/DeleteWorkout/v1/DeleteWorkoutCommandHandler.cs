using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sebastian.Api.Domain;

namespace Sebastian.Api.Features.Workouts.DeleteWorkout.v1
{
    public class DeleteWorkoutCommandHandler : AsyncRequestHandler<DeleteWorkoutCommand>
    {
        private readonly SebastianDbContext _db;

        public DeleteWorkoutCommandHandler(SebastianDbContext db)
        {
            _db = db;
        }

        protected override Task Handle(DeleteWorkoutCommand request, CancellationToken cancellationToken)
        {
            return _db.RunTransaction(() =>
             {
                 var workout = _db.Workouts.Find(request.WorkoutId);
                 _db.Remove(workout);
             });
        }
    }
}