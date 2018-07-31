using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Sebastian.Api.Features.Workouts.AddWorkout.v1
{
    public class AddWorkoutCommandHandler : IRequestHandler<AddWorkoutCommand, AddWorkoutResponse>
    {
        public Task<AddWorkoutResponse> Handle(AddWorkoutCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}