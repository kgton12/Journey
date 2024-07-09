using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception;

namespace Journey.Application.UseCases.Trips.Register
{
    public class RegisterTripValidator : AbstractValidator<RequestRegisterTripJson>
    {
        public RegisterTripValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_EMPTY);

            RuleFor(x => x.StartDate.Date).GreaterThanOrEqualTo(DateTime.Now.Date)
                .WithMessage(ResourceErrorMessages.DATE_TRIP_MUST_BE_LATER_THAN_TODAY);

            RuleFor(x => x).Must(x => x.EndDate >= x.StartDate)
                .WithMessage(ResourceErrorMessages.END_DATE_TRIP_MUST_BE_LATER_START_DATE);
        }
    }
}
