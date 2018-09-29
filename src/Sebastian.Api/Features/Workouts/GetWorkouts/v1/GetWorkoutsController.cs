using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Sebastian.Api.Features.Workouts.GetWorkouts.v1
{
    [Route("api/v1/workouts")]
    public class GetWorkoutsController : Controller
    {
        private readonly IMediator _mediator;

        public GetWorkoutsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Json(_mediator.Send(new GetWorkoutsQuery()).Result);
        }

        [HttpGet("{workoutId}")]
        public IActionResult Get(Guid workoutId)
        {
            if (workoutId == null || workoutId == Guid.Empty)
            {
                throw new ArgumentException();
            }

            return Json(_mediator.Send(new GetWorkoutsQuery {WorkoutId = workoutId}).Result);
        }
    }
}