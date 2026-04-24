using Reservationpitch.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Domain.Interfaces
{
    public interface IUnitofWork : IDisposable
    {
        IStadiumImageRepository StadiumImageRepository { get; }
        IImageRepository ImageRepository { get; }
        IStadiumCenterRepository StadiumCenterRepository { get; }
        IStadiumRepository StadiumRepository { get; }
        ITokenRepository TokenRepository { get; }
        IUserManagerRepository UserManagerRepository { get; }
        Task<int>SaveChangesAsync(CancellationToken cancellationToken = default); 
    }
}
