

using Reservationpitch.Application.Common.Handling;

namespace Reservationpitch.Application.Shared.Validations
{
    public interface IBaseValidation
    {
        public static string NameRegex = @"^[A-Za-z]+$";
        public static string EmailRegx { get; set; } = @"^[a-z0-9._%+-]+@[a-z]+\.[a-z]{2,}$";
        public static string PhoneNumberRegx { get; set; } = @"^\+[0-9.-]{9,}$";
        public static string IdRegex { get; set; } = @"^[0-9]{4,}$";
        Task<Result<string>> Namevalidate();
    }
}
