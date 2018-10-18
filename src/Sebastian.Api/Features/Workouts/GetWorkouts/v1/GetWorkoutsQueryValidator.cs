using System;
using System.Linq;
using FluentValidation;
using Sebastian.Api.Domain;
using Sebastian.Api.Infrastructure;

namespace Sebastian.Api.Features.Workouts.GetWorkouts.v1
{
    public class GetWorkoutsQueryValidator : AbstractValidator<GetWorkoutsQuery>
    {
        private readonly SebastianDbContext _db;
        private readonly IUserPrincipal _userPrincipal;

        public GetWorkoutsQueryValidator(SebastianDbContext db, IUserPrincipal userPrincipal)
        {
            _db = db;
            _userPrincipal = userPrincipal;

            RuleFor(x => x.WorkoutId)
                .Must(BelongToCurrentUser)
                .WithMessage("The workout requested must belong to you.");
        }

        private bool BelongToCurrentUser(Guid? workoutId)
        {
            if (workoutId == Guid.Empty || workoutId == null) return true;

            return _db.Workouts.Any(x => x.Id == workoutId && x.UserId == _userPrincipal.User.Id);
        }
    }
}
