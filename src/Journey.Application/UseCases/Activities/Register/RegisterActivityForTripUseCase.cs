using FluentValidation.Results;
using Journey.Communication.Enums;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Activities.Register
{
    public class RegisterActivityForTripUseCase
    {
        public async Task<ResponseActivityJson> Execute(Guid tripId, RequestRegisterActivityJson request)
        {
            var dbContext = new JourneyDbContext();

            var trip = await dbContext
                .Trips
                .FirstOrDefaultAsync(t => t.Id == tripId);

            Validate(trip, request);

            var entity = new Activity
            {
                Name = request.Name,
                Date = request.Date,
                TripId = tripId,
            };

            dbContext.Activities.Add(entity);
            await dbContext.SaveChangesAsync();

            return new ResponseActivityJson
            {
                Id = entity.Id,
                Name = entity.Name,
                Date = entity.Date,
                Status = (ActivityStatus)entity.Status,
            };
        }

        private static void Validate(Trip? trip, RequestRegisterActivityJson request)
        {
            if (trip is null) throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);

            var validator = new RegisterActivityValidator();

            var result = validator.Validate(request);

            if ((request.Date >= trip.StartDate && request.Date <= trip.EndDate) is false)
            {
                result.Errors.Add(new ValidationFailure("Date", ResourceErrorMessages.DATE_NOTE_WITHIN_TRAVER_PERIOD));
            }

            if (result.IsValid is false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
