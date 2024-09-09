
using Reservationpitch.Application.Common.Handling;

namespace Reservationpitch.Application.Shared.Validations
{
    public interface IPhoneNumberValidation
    {
        Task<Result<string>> PhoneNumbervalidate();
    }
}
