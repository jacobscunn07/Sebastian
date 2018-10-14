using MediatR;
using Sebastian.Api.Domain;
using Sebastian.Api.Features.Workouts.AddWorkout.v1;
using Shouldly;
using Sebastian.Api.Infrastructure;
using System;
using Sebastian.Api.Domain.Models;
using System.Threading.Tasks;

namespace Sebastian.Tests.Features.Workouts
{
    public class AddWorkoutCommandHandlerShould
    {
        private User _user;
        
        public async Task SetUp()
        {
            var db = Testing.Resolve<SebastianDbContext>();
            await db.RunTransaction(() =>
                {
                    db.Workouts.Clear();
                    _user = new User
                    {
                        Id = Guid.NewGuid(),
                        GivenName = "Test",
                        Surname = "Test"
                    };
                    db.Add(_user);
                });
        }
        
        public async Task SaveWorkout()
        {
            var mediator = Testing.Resolve<IMediator>();
            var db = Testing.Resolve<SebastianDbContext>();
            var command = new AddWorkoutCommand
            {
                Name = "Jacob's Awesome Workout",
                UserId = _user.Id
            };
            var addWorkoutResponse = await mediator.Send(command);
            var workout = db.Workouts.Find(addWorkoutResponse.Id);
            workout.Name.ShouldBe(command.Name);
        }

        public async Task NotSaveWorkoutWhenAnotherWorkoutInProgress()
        {
            var mediator = Testing.Resolve<IMediator>();

            var command1 = new AddWorkoutCommand
            {
                Name = "Workout 1",
                UserId = _user.Id
            };
            var command2 = new AddWorkoutCommand
            {
                Name = "Workout 2",
                UserId = _user.Id
            };

            await mediator.Send(command1);
            Should.Throw<InvalidSebastianOperationException>(async () => await mediator.Send(command2));
        }
    }
}
