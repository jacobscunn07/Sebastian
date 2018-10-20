using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sebastian.Api.Domain;
using Sebastian.Api.Infrastructure;

namespace Sebastian.Api.Features.Workouts.DeleteWorkout.v1
{
    [ApiExplorerSettings(GroupName = "Workouts")]
    [Route("api/v1/workouts")]
    public class DeleteWorkoutController : Controller
    {
        private readonly IMediator _mediator;
        private readonly SebastianDbContext _db;
        private readonly IUserPrincipal _userPrincipal;

        public DeleteWorkoutController(IMediator mediator, SebastianDbContext db, IUserPrincipal userPrincipal)
        {
            _mediator = mediator;
            _db = db;
            _userPrincipal = userPrincipal;
        }
        
        [HttpDelete("{workoutId}")]
        public async Task<IActionResult> Delete(Guid workoutId)
        {
            var command = new DeleteWorkoutCommand { WorkoutId = workoutId };
            var validator = new DeleteWorkoutCommandValidator(_db, _userPrincipal);
            var validation = validator.Validate(command);
            
            if (validation.IsValid)
            {
                var result = await _mediator.Send(command);
                return Ok();
            }

            return BadRequest(validation.Errors.Select(x => x.ErrorMessage));
        }
    }
}