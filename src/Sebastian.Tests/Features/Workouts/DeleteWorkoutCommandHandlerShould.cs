using MediatR;
using Sebastian.Api.Domain;
using Sebastian.Api.Domain.Models;
using Sebastian.Api.Features.Workouts.DeleteWorkout.v1;
using Sebastian.Api.Infrastructure;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;

namespace Sebastian.Tests.Features.Workouts
{
    public class DeleteWorkoutCommandHandlerShould
    {
        private User _user;

        public async Task SetUp()
        {
            var db = Testing.Resolve<SebastianDbContext>();
            _user = Mother.GetHydratedUser();
            db.Add(_user);
            await db.SaveChangesAsync();
            Testing.Resolve<IUserPrincipal>().User = _user;
        }

        public async Task SuccessfullyDeleteWorkout()
        {
            var mediator = Testing.Resolve<IMediator>();
            var db = Testing.Resolve<SebastianDbContext>();
            var workout = _user.Workouts.FirstOrDefault();
            var command = new DeleteWorkoutCommand { WorkoutId = workout.Id };
            await mediator.Send(command);
            var workouts = db.Workouts.Where(x => x.UserId == _user.Id).ToList();
            workouts.Any(x => x.Id == workout.Id).ShouldBeFalse();
        }
    }
}
