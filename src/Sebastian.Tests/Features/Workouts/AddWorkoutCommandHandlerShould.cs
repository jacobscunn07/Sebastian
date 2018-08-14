using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sebastian.Api.Domain;
using Sebastian.Api.Features.Workouts.AddWorkout.v1;
using Shouldly;
using System.Linq;

namespace Sebastian.Tests.Features.Workouts
{
    public class AddWorkoutCommandHandlerShould
    {
        public void SaveWorkout()
        {
            Testing.Action(async container =>
            {
                var mediator = container.GetService<IMediator>();
                var db = container.GetService<SebastianDbContext>();
                var command = new AddWorkoutCommand
                {
                    Name = "Jacob's Awesome Workout"
                };
                var addWorkoutResponse = await mediator.Send(command);
                var workout = db.Workouts.Find(addWorkoutResponse.Id);
                
                workout.Name.ShouldBe(command.Name);
            });
        }

        public void NotSaveWorkoutIfNameIsNull()
        {
            
        }
        
        public void NotSaveWorkoutIfNameIsEmpty()
        {
            
        }
    }
}
