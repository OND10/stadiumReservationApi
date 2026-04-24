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
    public class TimeSlotRepository : ITimeSlotRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitofWork _unitofWork;
        public TimeSlotRepository(ApplicationDbContext context, IUnitofWork unitofWork)
        {
            _context = context;
            _unitofWork = unitofWork;
        }


        public async Task GenerateTimeSlotsAsync(List<TimeSlots> timeSlots)
        {
            await _context.TimeSlots.AddRangeAsync(timeSlots);
            await _unitofWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<TimeSlots>> GetByCenterId(Guid centerId)
        {
            var result = await _context
                .TimeSlots
                .Where(t => t.CenterId == centerId)
                .ToListAsync();

            if (result.Count > 0)
            {
                return result;
            }

            return Enumerable.Empty<TimeSlots>();
        }

        public async Task<TimeSlots> GetLastTimeSlot(Guid centerId)
        {
            var result = await _context
                .TimeSlots
                .Where(t => t.CenterId == centerId)
                .OrderByDescending(t => t.DateOfTimeSlot)
                .FirstOrDefaultAsync();

            if (result == null)
            {
                throw new ModelNullException($"{result}", "TimeSlot is null with this cneterId");
            }

            return result;
        }
    }
}
