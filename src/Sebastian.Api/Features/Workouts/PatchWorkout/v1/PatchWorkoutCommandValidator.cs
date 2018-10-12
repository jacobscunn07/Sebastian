using FluentValidation;

namespace Sebastian.Api.Features.Workouts.PatchWorkout.v1
{
    public class PatchWorkoutCommandValidator : AbstractValidator<PatchWorkoutCommand>
    {
        public PatchWorkoutCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Workout name should not be empty");
        }
    }
}