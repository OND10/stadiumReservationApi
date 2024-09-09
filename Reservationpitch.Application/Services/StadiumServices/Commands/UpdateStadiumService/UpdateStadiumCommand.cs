using Reservationpitch.Application.Abstractions.Messaging;
using Reservationpitch.Application.DTOs.StadiumDTOs.Response;

namespace Reservationpitch.Application.Services.StadiumServices.Commands.UpdateStadiumService
{
    public class UpdateStadiumCommand : ICommand<StadiumResponseDto>
    {
        public Guid Id {  get; set; }
        public string Name { get; set; } = string.Empty;
        public double PriceinHour { get; set; }
        public string Type { get; set; } = string.Empty;
        public string NoOfplayers { get; set; } = null!;
        public Guid stadiumCenterId { get; set; }
    }
}
