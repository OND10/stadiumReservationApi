using Reservationpitch.Application.Common.Mappings.PitchMapper;
using Reservationpitch.Application.Services.StadiumReservationServices.Commands.CreateStadiumReservation;
using Reservationpitch.Domain.Entities;

namespace Reservationpitch.Application.DTOs.StadiumDTOs.Response
{
    public class StadiumReservationResponseDto : IMap<StadiumReservation>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string UserPhoneNumber { get; set; }
        public DateTime Date { get; set; }
        public TimeOnly DateTime { get; set; }
        public float Price { get; set; }
        public string Payment { get; set; }

        public StadiumReservationResponseDto FromModel(StadiumReservation entity)
        {
            return new StadiumReservationResponseDto
            {
                Id = entity.Id,
                UserName = entity.UserName,
                UserPhoneNumber = entity.UserPhoneNumber,
                Date = entity.Date,
                Price = entity.Price,
                Payment = entity.Payment,

            };
        }
    }
}
