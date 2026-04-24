using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Reservationpitch.Domain.Common.Enums;
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
    public class CenterBooking : AuditableEntity
    {
        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        [Required]
        public double TotalPrice { get; set; } // Ensure price calculation is enforced.

        [Required]
        [AllowedValues("Kurami", "AlNajm", "OnArrived")]
        public PaymentMethod PaymentMethod { get; set; }

        public string? ExchangeNumber { get; set; } // Optional for specific payment methods.

        [Required]
        public DateTime BeginTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.UtcNow;

        [Required]
        public Guid CenterId { get; set; }

        [ForeignKey(nameof(CenterId))]
        public virtual StadiumCenter StadiumCenter { get; set; }

        public virtual ICollection<StadiumReservation> StadiumReservations { get; set; } = new List<StadiumReservation>();
    }

}
