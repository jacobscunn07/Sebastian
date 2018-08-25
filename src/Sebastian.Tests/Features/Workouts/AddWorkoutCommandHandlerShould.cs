using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sebastian.Api.Domain;
using Sebastian.Api.Features.Workouts.AddWorkout.v1;
using Shouldly;
using Sebastian.Api.Infrastructure;
using System;
using Sebastian.Api.Domain.Models;

namespace Sebastian.Tests.Features.Workouts
{
    public class AddWorkoutCommandHandlerShould
    {
        private User _user;
        
        public void SetUp()
        {
            Testing.Action(container =>
            {
                var db = container.GetService<SebastianDbContext>();
                db.RunTransaction(() =>
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
            });
        }
        
        public void SaveWorkout()
        {
            Testing.Action(async container =>
            {
                var mediator = container.GetService<IMediator>();
                var db = container.GetService<SebastianDbContext>();
                var command = new AddWorkoutCommand
                {
                    Name = "Jacob's Awesome Workout",
                    UserId = _user.Id
                };
                var addWorkoutResponse = await mediator.Send(command);
                var workout = db.Workouts.Find(addWorkoutResponse.Id);
                
                workout.Name.ShouldBe(command.Name);
            });
        }

        public void NotSaveWorkoutWhenAnotherWorkoutInProgress()
        {
            Testing.Action(async container =>
            {
                var mediator = container.GetService<IMediator>();
                
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
            });
        }
    }
}
