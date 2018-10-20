using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Sebastian.Api.Features.Workouts.PatchWorkout.v1
{
    [ApiExplorerSettings(GroupName = "Workouts")]
    [Route("api/v1/workouts")]
    public class PatchWorkoutController : Controller
    {
        private readonly IMediator _mediator;

        public PatchWorkoutController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPatch("")]
        public IActionResult Index(PatchWorkoutCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            }
            
            var result = _mediator.Send(command);
            return Ok();
        }
    }
}