using Sebastian.Api.Domain;
using Sebastian.Api.Domain.Models;
using Sebastian.Api.Features.Workouts.GetWorkouts.v1;
using Sebastian.Api.Infrastructure;
using Shouldly;
using System;
using System.Threading.Tasks;

namespace Sebastian.Tests.Features.Workouts
{
    public class GetWorkoutsQueryValidatorShould
    {
        private User _user;

        public void SetUp()
        {
            var db = Testing.Resolve<SebastianDbContext>();
            _user = Mother.GetHydratedUser();
            db.Add(_user);
            Testing.Resolve<IUserPrincipal>().User = _user;
        }

        public async Task ValidateSuccessfully()
        {
            var db = Testing.Resolve<SebastianDbContext>();
            var validator = new GetWorkoutsQueryValidator(db, Testing.Resolve<IUserPrincipal>());
            var workout = new Workout
            {
                Id = Guid.NewGuid(),
                UserId = _user.Id,
                Name = "My workout",
                DateTimeBegan = DateTime.UtcNow
            };
            db.Workouts.Add(workout);
            await db.SaveChangesAsync();
            var command = new GetWorkoutsQuery { WorkoutId = workout.Id };
            var result = validator.Validate(command);
            result.IsValid.ShouldBe(true);
        }

        public void FailToFindWorkoutAssociatedWithUser()
        {
            var db = Testing.Resolve<SebastianDbContext>();
            var validator = new GetWorkoutsQueryValidator(db, Testing.Resolve<IUserPrincipal>());
            var workout = new Workout
            {
                Id = Guid.NewGuid(),
                UserId = _user.Id,
                Name = "My workout",
                DateTimeBegan = DateTime.UtcNow
            };
            var command = new GetWorkoutsQuery { WorkoutId = workout.Id };
            var result = validator.Validate(command);
            result.IsValid.ShouldBe(false);
        }
    }
}
