using Reservationpitch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.DTOs.WorkDayDTOs.Request
{
    public class WorkDayRequestDto
    {
        public Guid stadiumCenterId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeOnly? BeginWorkTime { get; set; } 
        public TimeOnly? EndWorkTime { get; set; }
    }
}
