using Journey.Application.UseCases.Trips.GetAll;
using Journey.Application.UseCases.Trips.Register;
using Journey.Communication.Requests;
using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] RequestRegisterTripJson request)
        {
            try
            {
                var useCase = new RegisterTripUseCase();

                var response = await useCase.Execute(request);

                return Created(string.Empty, response);
            }
            catch (JorneyException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unknown error");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var useCase = new GetAllTripsUseCase();

            var response = await useCase.Execute();

            return Ok(response);
        }
    }
}
