using Reservationpitch.Application.Common.Handling;

namespace Reservationpitch.Application.Shared.Validations
{
    public interface IPasswordValidation
    {
        Task<Result<string>> Passwordvalidate();
    }
}
