using Microsoft.AspNetCore.Mvc;

namespace Sebastian.Api.Features.Workouts.AddWorkout.v1
{
    public class AddWorkoutController : Controller
    {
        [HttpPost("api/v1/workouts")]
        public IActionResult Index()
        {
            return Json(new {value = "test"});
        }
    }
}
