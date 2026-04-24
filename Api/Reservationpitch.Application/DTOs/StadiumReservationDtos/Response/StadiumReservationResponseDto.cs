using Reservationpitch.Application.Common.Mappings.PitchMapper;
using Reservationpitch.Application.Services.StadiumReservationServices.Commands.CreateStadiumReservation;
using Reservationpitch.Domain.Common.Enums;
using Reservationpitch.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Reservationpitch.Application.DTOs.StadiumDTOs.Response
{
    public class StadiumReservationResponseDto : IMap<StadiumReservation>
    {
         
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

         
        public double TotalPrice { get; set; } // Ensure price calculation is enforced.

         
        public PaymentMethod PaymentMethod { get; set; }

        public string? ExchangeNumber { get; set; } // Optional for specific payment methods.

         
        public DateTime BeginTime { get; set; }

         
        public DateTime EndTime { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.UtcNow;

         
        public Guid CenterId { get; set; }

        [ForeignKey(nameof(CenterId))]
        public virtual StadiumCenter StadiumCenter { get; set; }

        public virtual ICollection<StadiumReservation> StadiumReservations { get; set; } = new List<StadiumReservation>();

        public StadiumReservationResponseDto FromModel(CenterBooking entity)
        {
            return new StadiumReservationResponseDto
            {
                User = entity.User,
                StadiumCenter = entity.StadiumCenter,
                StadiumReservations = entity.StadiumReservations,
                BookingDate = entity.BookingDate,
                BeginTime = entity.BeginTime,
                EndTime = entity.EndTime,
                CenterId = entity.CenterId,
                ExchangeNumber = entity.ExchangeNumber,
                PaymentMethod = entity.PaymentMethod,
                TotalPrice = entity.TotalPrice,
                UserId = entity.UserId,
               
            };
        }
    }
}
