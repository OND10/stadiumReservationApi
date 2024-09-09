using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Domain.Interfaces
{
    public interface IUnitofWork
    {
        Task<int>SaveChangesAsync(CancellationToken cancellationToken = default); 
    }
}
