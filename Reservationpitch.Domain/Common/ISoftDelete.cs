using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Domain.Common
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
        string? DeletedBy { get; set; }
        DateTime? DeletedAt { get; set; }
    }
}
