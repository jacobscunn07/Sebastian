using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Sebastian.Api.Features.Workouts.AddWorkout.v1
{
    public class AddWorkoutController : Controller
    {
        private readonly IMediator _mediator;

        public AddWorkoutController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost("api/v1/workouts")]
        public IActionResult Index()
        {
            _mediator.Send(new AddWorkoutCommand { Name = "test" });
            return Json(new {value = "test"});
        }
    }
}
