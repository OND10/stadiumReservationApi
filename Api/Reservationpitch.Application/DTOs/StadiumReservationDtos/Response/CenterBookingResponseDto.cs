using Reservationpitch.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.DTOs.StadiumReservationDtos.Response
{
    public class CenterBookingResponseDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string CenterName { get; set; }
        public double TotalPrice { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
