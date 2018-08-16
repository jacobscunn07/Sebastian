using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Index([FromBody]AddWorkoutCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            }
            
            var response = await _mediator.Send(command);
            return Json(response);
        }
    }
}
