using Reservationpitch.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.DTOs.StadiumReservationDtos.Request
{
    public class CenterBookingDto
    {
        public string UserId { get; set; }
        public Guid CenterId { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string? ExchangeNumber { get; set; }
    }
}
