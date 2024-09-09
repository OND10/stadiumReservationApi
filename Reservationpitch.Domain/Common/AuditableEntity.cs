using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Domain.Common
{
    public class AuditableEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public DateTime DeletedOn { get; set; }
    }
}
