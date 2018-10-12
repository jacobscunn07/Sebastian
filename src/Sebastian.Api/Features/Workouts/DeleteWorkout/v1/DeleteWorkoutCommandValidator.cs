using System;
using FluentValidation;
using Sebastian.Api.Domain;

namespace Sebastian.Api.Features.Workouts.DeleteWorkout.v1
{
    public class DeleteWorkoutCommandValidator : AbstractValidator<DeleteWorkoutCommand>
    {
        private readonly SebastianDbContext _db;

        public DeleteWorkoutCommandValidator(SebastianDbContext db)
        {
            _db = db;
            
            RuleFor(x => x.WorkoutId)
                .NotEmpty()
                .NotNull()
                .WithMessage("Workout Id should not be empty.")
                .Must(Exist)
                .WithMessage("A workout does not exist with workout id");
        }

        private bool Exist(Guid workoutId)
        {
            var workout = _db.Workouts.Find(workoutId);
            return workout != null;
        }
    }
}