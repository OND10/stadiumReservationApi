using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.DTOs.StadiumDTOs.Request
{
    public class StadiumReservationRequestDto
    {
        public string UserName { get; set; }
        public string UserPhoneNumber { get; set; }
        public DateTime Date { get; set; }
        public TimeOnly DateTime { get; set; }
        public float Price { get; set; }
        public string Payment { get; set; }
    }
}
