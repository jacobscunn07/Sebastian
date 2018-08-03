using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sebastian.Api.Features.Workouts.AddWorkout.v1;

namespace Sebastian.Tests.Features.Workouts
{
    public class AddWorkoutCommandHandlerShould
    {
        public void SaveWorkout()
        {
            Testing.Action(container =>
            {
                var mediator = container.GetService<IMediator>();
                var command = new AddWorkoutCommand
                {
                    Name = "Jacob's Awesome Workout"
                };
                mediator.Send(command);
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
