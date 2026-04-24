using Reservationpitch.Application.Common.Handling;

namespace Reservationpitch.Application.Shared.Validations
{
    public interface IEmailValidation
    {
        Task<Result<string>> Emailvalidate();
    }
}
