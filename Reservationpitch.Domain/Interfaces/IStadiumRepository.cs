using Reservationpitch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Domain.Interfaces
{
    public interface IStadiumRepository: IGenericRepository<Stadium>, IUnitofWork
    {
        Task<Stadium> UpdateAsync(Stadium entity, CancellationToken cancellationToken);
        Task<IEnumerable<Stadium>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Stadium>> GetAllStadiumByCenterId(Guid centerId, CancellationToken cancellationToken);
    }
}
