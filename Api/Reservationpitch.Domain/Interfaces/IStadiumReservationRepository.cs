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
        Task<CenterBooking> CreateBookingAsync(CenterBooking booking);
        Task<IEnumerable<CenterBooking>> GetBookingsAsync();
        Task<CenterBooking?> GetBookingByIdAsync(Guid id);
        Task UpdateBookingAsync(CenterBooking booking);
        Task DeleteBookingAsync(Guid id);
    }
}
