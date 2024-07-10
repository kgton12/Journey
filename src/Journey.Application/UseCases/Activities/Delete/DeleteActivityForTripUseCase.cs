using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Activities.Delete
{
    public class DeleteActivityForTripUseCase
    {
        public async Task Execute(Guid tripId, Guid activityId)
        {
            var dbContext = new JourneyDbContext();

            var activity = await dbContext
                .Activities
                .FirstOrDefaultAsync(x => x.Id == activityId && x.TripId == tripId)
                ?? throw new NotFoundException(ResourceErrorMessages.ACTIVITY_NOT_FOUND);

            dbContext.Activities.Remove(activity);
            await dbContext.SaveChangesAsync();
        }
    }
}
