using Sebastian.Api.Domain;
using Sebastian.Api.Domain.Models;
using Sebastian.Api.Features.Workouts.DeleteWorkout.v1;
using Sebastian.Api.Infrastructure;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sebastian.Tests.Features.Workouts
{
    public class DeleteWorkoutCommandValidatorShould
    {
        private User _user;

        public async Task SetUp()
        {
            var db = Testing.Resolve<SebastianDbContext>();
            _user = Mother.GetHydratedUser();
            db.Users.Add(_user);
            await db.SaveChangesAsync();
            Testing.Resolve<IUserPrincipal>().User = _user;
        }

        public void ValidateSuccessfully()
        {
            var db = Testing.Resolve<SebastianDbContext>();
            var validator = new DeleteWorkoutCommandValidator(db, Testing.Resolve<IUserPrincipal>());
            var workout = _user.Workouts.FirstOrDefault();
            var command = new DeleteWorkoutCommand { WorkoutId = workout.Id };
            var result = validator.Validate(command);
            result.IsValid.ShouldBe(true);
        }

        public void FailFromInvalidWorkoutId()
        {
            var db = Testing.Resolve<SebastianDbContext>();
            var validator = new DeleteWorkoutCommandValidator(db, Testing.Resolve<IUserPrincipal>());
            var command = new DeleteWorkoutCommand { WorkoutId = Guid.NewGuid() };
            var result = validator.Validate(command);
            result.IsValid.ShouldBe(false);
        }

        public void FailFromEmptyWorkoutId()
        {
            var db = Testing.Resolve<SebastianDbContext>();
            var validator = new DeleteWorkoutCommandValidator(db, Testing.Resolve<IUserPrincipal>());
            var command = new DeleteWorkoutCommand { WorkoutId = Guid.Empty };
            var result = validator.Validate(command);
            result.IsValid.ShouldBe(false);
        }
    }
}
