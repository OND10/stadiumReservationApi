using Reservationpitch.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Domain.Entities
{
    public class StadiumImages:AuditableEntity
    {
        public string FileName { get; set; } = null!;
        public string FileExtension { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string Title { get; set; } = null!;
        public Guid StadiumId { get; set; }
    }
}
