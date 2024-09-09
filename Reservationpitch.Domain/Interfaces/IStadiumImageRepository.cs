using Microsoft.AspNetCore.Http;
using Reservationpitch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Domain.Interfaces
{
    public interface IStadiumImageRepository
    {
        Task<StadiumImages> UploadAsync(IEnumerable<IFormFile> files, Guid pitchId, string rootPath);
        Task<IEnumerable<StadiumImages>> FindAllAsync(Expression<Func<StadiumImages, bool>> expression, CancellationToken cancellationToken);
        Task<IEnumerable<StadiumImages>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<StadiumImages>> GetByStadiumIdAsync(Guid id, CancellationToken cancellationToken);

    }
}
