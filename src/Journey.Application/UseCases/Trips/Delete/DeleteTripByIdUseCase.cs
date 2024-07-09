using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Trips.Delete
{
    public class DeleteTripByIdUseCase
    {
        public async Task Execute(Guid id)
        {
            var dbContext = new JourneyDbContext();

            var trip = await dbContext
                .Trips
                .Include(x => x.Activities)
                .FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);

            dbContext.Trips.Remove(trip);
            await dbContext.SaveChangesAsync();
        }
    }
}
