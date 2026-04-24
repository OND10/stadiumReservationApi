using Microsoft.AspNetCore.Http;
using Reservationpitch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Domain.Interfaces
{
    public interface IStadiumCenterRepository
    {
        Task<IEnumerable<StadiumCenter>> GetAllAsync(CancellationToken cancellationToken);
        Task<StadiumCenter> CreateAsync(StadiumCenter entity, CancellationToken cancellationToken);
        Task<StadiumCenter> UpdateAsync(StadiumCenter entity, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<StadiumCenter> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<string> Upload(StadiumCenter model, IFormFile file);
    }
}
