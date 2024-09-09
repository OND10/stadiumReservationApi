using Reservationpitch.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Domain.Entities
{
    public class Stadium:AuditableEntity
    {
        public string Name {  get; set; } = string.Empty;
        public double PriceinHour { get; set; }
        public string Type { get; set; } = string.Empty;
        public string NoOfplayers { get; set; } = null!;
        public Guid stadiumCenterId { get; set; }

    }
}
