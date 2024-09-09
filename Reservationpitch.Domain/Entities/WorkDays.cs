using Reservationpitch.Domain.Common;
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
        public string BeginWorkTime { get; set; } = string.Empty;
        public string EndWorkTime { get; set; } = string.Empty;
    }
}
