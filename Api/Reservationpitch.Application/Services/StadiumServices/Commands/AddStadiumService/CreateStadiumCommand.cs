using Reservationpitch.Application.Abstractions.Messaging;
using Reservationpitch.Application.DTOs.StadiumDTOs.Response;

namespace Reservationpitch.Application.Services.StadiumServices.Commands.AddStadiumService
{
    public class CreateStadiumCommand : ICommand<StadiumResponseDto>
    {
        public string Name { get; set; } = string.Empty;
        public double PriceinHour { get; set; }
        public string Type { get; set; } = string.Empty;
        public string NoOfplayers { get; set; } = null!;
        public Guid stadiumCenterId { get; set; }
    }
}
