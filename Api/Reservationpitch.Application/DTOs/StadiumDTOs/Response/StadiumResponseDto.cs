using Reservationpitch.Application.Common.Mappings.PitchMapper;
using Reservationpitch.Domain.Entities;

namespace Reservationpitch.Application.DTOs.StadiumDTOs.Response
{
    public class StadiumResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double PriceinHour { get; set; }
        public string Type { get; set; } = string.Empty;
        public string NoOfplayers { get; set; } = null!;
        public Guid stadiumCenterId { get; set; }
    }
}
