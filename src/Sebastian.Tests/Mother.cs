using FizzWare.NBuilder;
using FizzWare.NBuilder.Implementation;
using Sebastian.Api.Domain.Models;
using System.Linq;

namespace Sebastian.Tests
{
    public static class Mother
    {
        public static User GetHydratedUser()
        {
            return Builder<User>.CreateNew()
                .With(x => x.Workouts = Builder<Workout>.CreateListOfSize(5).All().Build().ToList())
                .Build();
        }
    }
}
