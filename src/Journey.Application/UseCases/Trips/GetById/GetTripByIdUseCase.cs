using Journey.Communication.Enums;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.GetById
{
    public class GetTripByIdUseCase
    {
        public async Task<ResponseTripJson> Execute(Guid Id)
        {
            var dbContext = new JourneyDbContext();

            var trip = await dbContext
                .Trips
                .Include(t => t.Activities)
                .FirstOrDefaultAsync(x => x.Id == Id);

            return trip is null ? throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND) :
             new ResponseTripJson
             {
                 Id = trip.Id,
                 Name = trip.Name,
                 StartDate = trip.StartDate,
                 EndDate = trip.EndDate,
                 Activities = trip.Activities.Select(a => new ResponseActivityJson
                 {
                     Id = a.Id,
                     Name = a.Name,
                     Date = a.Date,
                     Status = (ActivityStatus)a.Status
                 }).ToList()
             };
        }
    }
}
