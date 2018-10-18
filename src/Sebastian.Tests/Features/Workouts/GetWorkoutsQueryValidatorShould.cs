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

        public async Task SetUp()
        {
            var db = Testing.Resolve<SebastianDbContext>();
            await db.RunTransaction(() =>
            {
                db.Workouts.Clear();
                _user = new User
                {
                    Id = Guid.NewGuid(),
                    GivenName = "Test",
                    Surname = "Test"
                };
                db.Add(_user);
            });
            Testing.Resolve<IUserPrincipal>().User = _user;
        }

        public async Task ValidateSuccessfully()
        {
            var db = Testing.Resolve<SebastianDbContext>();
            var validator = new GetWorkoutsQueryValidator(db, Testing.Resolve<IUserPrincipal>());
            var workout = new Workout
            {
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

        public async Task DisallowUserToViewAnotherUsersWorkout()
        {
            var db = Testing.Resolve<SebastianDbContext>();
            var workout = new Workout
            {
                UserId = _user.Id,
                Name = "My workout",
                DateTimeBegan = DateTime.UtcNow
            };
            db.Workouts.Add(workout);
            await db.SaveChangesAsync();
            var command = new GetWorkoutsQuery { WorkoutId = workout.Id };
            var anotherUser = new User
            {
                Id = Guid.NewGuid(),
                GivenName = "Test1",
                Surname = "Test2"
            };
            db.Add(anotherUser);
            var userPrincipal = Testing.Resolve<IUserPrincipal>();
            userPrincipal.User = anotherUser;
            var validator = new GetWorkoutsQueryValidator(db, userPrincipal);
            var result = validator.Validate(command);
            result.IsValid.ShouldBe(false);
        }
    }
}
