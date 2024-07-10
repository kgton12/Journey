using Journey.Application.UseCases.Activities.Complete;
using Journey.Application.UseCases.Activities.Delete;
using Journey.Application.UseCases.Activities.Register;
using Journey.Application.UseCases.Trips.Delete;
using Journey.Application.UseCases.Trips.GetAll;
using Journey.Application.UseCases.Trips.GetById;
using Journey.Application.UseCases.Trips.Register;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseShortTripJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RequestRegisterTripJson request)
        {
            var useCase = new RegisterTripUseCase();

            var response = await useCase.Execute(request);

            return Created(string.Empty, response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseTripsJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var useCase = new GetAllTripsUseCase();

            var response = await useCase.Execute();

            return Ok(response);
        }

        [HttpGet]
        [Route("{Id}")]
        [ProducesResponseType(typeof(ResponseTripJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid Id)
        {
            var useCase = new GetTripByIdUseCase();

            var response = await useCase.Execute(Id);

            return Ok(response);
        }

        [HttpDelete]
        [Route("{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid Id)
        {
            var useCase = new DeleteTripByIdUseCase();

            await useCase.Execute(Id);

            return NoContent();
        }

        [HttpPost]
        [Route("{tripId}/activity")]
        [ProducesResponseType(typeof(ResponseActivityJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RegisterActivity([FromRoute] Guid tripId, [FromBody] RequestRegisterActivityJson request)
        {
            var useCase = new RegisterActivityForTripUseCase();

            var response = await useCase.Execute(tripId, request);

            return Created(string.Empty, response);
        }

        [HttpPut]
        [Route("{tripId}/activity/{activityId}/complete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CompleteActivity([FromRoute] Guid tripId, [FromRoute] Guid activityId)
        {
            var useCase = new CompleteActivityForTripUseCase();

            await useCase.Execute(tripId, activityId);

            return NoContent();
        }

        [HttpDelete]
        [Route("{tripId}/activity/{activityId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorsJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteActivity([FromRoute] Guid tripId, [FromRoute] Guid activityId)
        {
            var useCase = new DeleteActivityForTripUseCase();

            await useCase.Execute(tripId, activityId);

            return NoContent();
        }
    }
}
