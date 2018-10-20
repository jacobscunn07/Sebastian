using System;
using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sebastian.Api.Domain;

namespace Sebastian.Api.Features.Workouts.DeleteWorkout.v1
{
    [ApiExplorerSettings(GroupName = "Workouts")]
    [Route("api/v1/workout")]
    public class DeleteWorkoutController : Controller
    {
        private readonly IMediator _mediator;
        private readonly SebastianDbContext _db;


        public DeleteWorkoutController(IMediator mediator, SebastianDbContext db)
        {
            _mediator = mediator;
            _db = db;
        }

        
        [HttpDelete("{workoutId}")]
        public IActionResult Delete(Guid workoutId)
        {
            var command = new DeleteWorkoutCommand { WorkoutId = workoutId };
            var validator = new DeleteWorkoutCommandValidator(_db);
            var validation = validator.Validate(command);
            
            if (validation.IsValid)
            {
                var result = _mediator.Send(command).Result;
                return Ok();
            }

            return BadRequest(validation.Errors.Select(x => x.ErrorMessage));
        }
    }
}