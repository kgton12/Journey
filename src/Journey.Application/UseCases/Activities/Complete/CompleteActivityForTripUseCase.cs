using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Activities.Complete
{
    public class CompleteActivityForTripUseCase
    {
        public async Task Execute(Guid tripId, Guid activityId)
        {
            var dbContext = new JourneyDbContext();

            var activity = await dbContext
                .Activities
                .FirstOrDefaultAsync(x => x.Id == activityId && x.TripId == tripId)
                ?? throw new NotFoundException(ResourceErrorMessages.ACTIVITY_NOT_FOUND);

            activity.Status = ActivityStatus.Done;

            dbContext.Activities.Update(activity);
            await dbContext.SaveChangesAsync();
        }
    }
}
