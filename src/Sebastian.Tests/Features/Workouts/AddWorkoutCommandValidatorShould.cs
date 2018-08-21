using Sebastian.Api.Features.Workouts.AddWorkout.v1;
using Shouldly;

namespace Sebastian.Tests.Features.Workouts
{
    public class AddWorkoutCommandValidatorShould
    {
        
        public void ValidateSuccessfully()
        {
            var validator = new AddWorkoutCommandValidator();
            var command = new AddWorkoutCommand { Name = "Workout 1" };
            var result = validator.Validate(command);
            result.IsValid.ShouldBe(true);
        }

        public void NameNotBeEmpty()
        {
            var validator = new AddWorkoutCommandValidator();
            var command = new AddWorkoutCommand { Name = "" };
            var result = validator.Validate(command);
            result.IsValid.ShouldBe(false);
        }

        public void NameNotBeNull()
        {
            var validator = new AddWorkoutCommandValidator();
            var command = new AddWorkoutCommand { Name = null };
            var result = validator.Validate(command);
            result.IsValid.ShouldBe(false);
        }
    }
}