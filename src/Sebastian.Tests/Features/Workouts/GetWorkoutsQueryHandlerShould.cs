﻿using MediatR;
using Sebastian.Api.Domain;
using Sebastian.Api.Domain.Models;
using Sebastian.Api.Features.Workouts.GetWorkouts.v1;
using System;
using System.Threading.Tasks;
using System.Linq;
using Shouldly;

namespace Sebastian.Tests.Features.Workouts
{
    public class GetWorkoutsQueryHandlerShould
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
        }

        public async Task GetUserWorkoutById()
        {
            var db = Testing.Resolve<SebastianDbContext>();
            var mediator = Testing.Resolve<IMediator>();

            _user.Workouts.Add(new Workout
            {
                Name = "First Workout",
                DateTimeBegan = DateTime.UtcNow,
            });
            await db.SaveChangesAsync();
            var workout = _user.Workouts.FirstOrDefault();
            var query = new GetWorkoutsQuery { UserId = _user.Id, WorkoutId = workout.Id };
            var result = mediator.Send(query).Result;

            result.Workouts.ToList().Count.ShouldBe(_user.Workouts.Count);
            result.Workouts.FirstOrDefault().Id.ShouldBe(workout.Id);
        }

        public async Task GetUserWorkouts()
        {
            var db = Testing.Resolve<SebastianDbContext>();
            var mediator = Testing.Resolve<IMediator>();

            _user.Workouts.Add(new Workout
            {
                Name = "First Workout",
                DateTimeBegan = DateTime.UtcNow,
            });
            _user.Workouts.Add(new Workout
            {
                Name = "Second Workout",
                DateTimeBegan = DateTime.UtcNow,
            });
            await db.SaveChangesAsync();
            var query = new GetWorkoutsQuery { UserId = _user.Id };
            var result = mediator.Send(query).Result;

            result.Workouts.Count().ShouldBe(_user.Workouts.Count());
        }
    }
}
