using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sebastian.Api.Infrastructure;

namespace Sebastian.Api.Features.Workouts.AddWorkout.v1
{
    public class AddWorkoutController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserPrincipal _userPrincipal;

        public AddWorkoutController(IMediator mediator, UserPrincipal userPrincipal)
        {
            _mediator = mediator;
            _userPrincipal = userPrincipal;
        }
        
        [HttpPost("api/v1/workouts")]
        public async Task<IActionResult> Index([FromBody]AddWorkoutCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            }

            command.UserId = _userPrincipal.User.Id;
            
            var response = await _mediator.Send(command);
            return Json(response);
        }
    }
}
