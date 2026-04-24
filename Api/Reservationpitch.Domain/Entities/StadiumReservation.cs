using Reservationpitch.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Domain.Entities
{
    public class StadiumReservation : AuditableEntity
    {
        [Required]
        public Guid StadiumId { get; set; }

        [ForeignKey(nameof(StadiumId))]
        public virtual Stadium Stadium { get; set; }

        [Required]
        public Guid CenterBookingId { get; set; }

        [ForeignKey(nameof(CenterBookingId))]
        public virtual CenterBooking CenterBooking { get; set; }
    }
}
