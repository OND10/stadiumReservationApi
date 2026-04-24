using Reservationpitch.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Domain.Entities
{
    public class WorkDays : AuditableEntity
    {
        public Guid stadiumCenterId { get; set; }
        public string DayOfWeek { get; set; } = string.Empty;
        public TimeOnly? BeginWorkTime { get; set; }
        public TimeOnly? EndWorkTime { get; set; } 
        public TimeOnly? StartBreakingTime { get; set; }
        public TimeOnly? EndBreakingTime { get; set; }
    }
}
