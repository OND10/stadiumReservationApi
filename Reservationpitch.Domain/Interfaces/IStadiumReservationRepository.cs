using Reservationpitch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Domain.Interfaces
{
    public interface IStadiumReservationRepository
    {
        Task<IEnumerable<StadiumReservation>> GetAllAsync(CancellationToken cancellationToken);
        Task<StadiumReservation> CreateAsync(StadiumReservation entity, CancellationToken cancellationToken);
        Task<StadiumReservation> UpdateAsync(StadiumReservation entity, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<StadiumReservation> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
