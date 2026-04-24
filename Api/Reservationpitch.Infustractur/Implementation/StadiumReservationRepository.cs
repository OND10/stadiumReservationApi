using Microsoft.EntityFrameworkCore;
using Reservationpitch.Domain.Common.Exceptions;
using Reservationpitch.Domain.Entities;
using Reservationpitch.Domain.Interfaces;
using Reservationpitch.Infustractur.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Infustractur.Implementation
{
    public class StadiumReservationRepository : IStadiumReservationRepository
    {
        private readonly ApplicationDbContext _context;

        public StadiumReservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<CenterBooking> CreateBookingAsync(CenterBooking booking)
        {
            _context.CenterBookings.Add(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<IEnumerable<CenterBooking>> GetBookingsAsync()
        {
            var result = await _context.CenterBookings
                .Include(b => b.User)
                .Include(b => b.StadiumCenter)
                .Include(b => b.StadiumReservations)
                .AsNoTracking()
                .AsSplitQuery()
                .ToListAsync();

            return result;
        }

        public async Task<CenterBooking?> GetBookingByIdAsync(Guid id)
        {

            var result = await _context.CenterBookings
                  .Include(b => b.StadiumCenter)
                  .Include(b => b.StadiumReservations)
                  .AsNoTracking()
                  .AsSplitQuery()
                  .FirstOrDefaultAsync(b => b.Id == id);

            if (result == null)
            {
                throw new IdNullException(nameof(result));
            }

            return result;
        }

        public async Task UpdateBookingAsync(CenterBooking booking)
        {
            _context.CenterBookings.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBookingAsync(Guid id)
        {
            var booking = await _context.CenterBookings.FindAsync(id);
            if (booking != null)
            {
                _context.CenterBookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
        }

    }
}
