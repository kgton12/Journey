using Journey.Communication.Responses;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.GetAll
{
    public class GetAllTripsUseCase
    {
        public async Task<ResponseTripsJson> Execute()
        {
            var dbContext = new JourneyDbContext();

            var trips = await dbContext.Trips.ToListAsync();

            return new ResponseTripsJson
            {
                Trips = trips.Select(t => new ResponseShortTripJson
                {
                    Id = t.Id,
                    Name = t.Name,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                }).ToList()
            };
        }
    }
}
