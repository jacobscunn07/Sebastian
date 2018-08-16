using FluentValidation;

namespace Sebastian.Api.Features.Workouts.AddWorkout.v1
{
    public class AddWorkoutCommandValidator : AbstractValidator<AddWorkoutCommand>
    {
        public AddWorkoutCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Workout name should not be empty");
        }
    }
}
