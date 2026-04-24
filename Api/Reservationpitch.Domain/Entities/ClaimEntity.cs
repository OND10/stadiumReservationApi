using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Domain.Entities
{
    public class ClaimEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Type { get; set; } = default!;
        public string Value { get; set; } = default!;
    }
}
