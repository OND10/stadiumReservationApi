using Reservationpitch.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Domain.Entities
{
    public class TimeSlots : AuditableEntity
    {
        public Guid CenterId { get; set; }
        [ForeignKey(nameof(CenterId))]
        public virtual StadiumCenter StadiumCenter { get; set; }

        public DateOnly DateOfTimeSlot { get; set; }

        public TimeOnly TimeSlotValue { get; set; }

        public string? TimeStatus { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<CenterBooking> Appointments { get; set; } = new List<CenterBooking>();



    }
}
