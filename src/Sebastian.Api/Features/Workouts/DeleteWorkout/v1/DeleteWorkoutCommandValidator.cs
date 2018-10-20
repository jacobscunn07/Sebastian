using System;
using System.Linq;
using FluentValidation;
using Sebastian.Api.Domain;
using Sebastian.Api.Infrastructure;

namespace Sebastian.Api.Features.Workouts.DeleteWorkout.v1
{
    public class DeleteWorkoutCommandValidator : AbstractValidator<DeleteWorkoutCommand>
    {
        private readonly SebastianDbContext _db;
        private readonly IUserPrincipal _userPrincipal;

        public DeleteWorkoutCommandValidator(SebastianDbContext db, IUserPrincipal userPrincipal)
        {
            _db = db;
            _userPrincipal = userPrincipal;

            RuleFor(x => x.WorkoutId)
                .NotEmpty()
                .NotNull()
                .WithMessage("Workout Id should not be empty.")
                .Must(BelongToCurrentUser)
                .WithMessage("The workout you are trying to delete should belong to you.");
        }

        private bool BelongToCurrentUser(Guid workoutId)
        {
            return _db.Workouts.Any(x => x.Id == workoutId && x.UserId == _userPrincipal.User.Id);
        }
    }
}