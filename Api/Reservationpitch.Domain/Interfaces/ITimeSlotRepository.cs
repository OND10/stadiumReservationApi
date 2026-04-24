using Reservationpitch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Domain.Interfaces
{
    public interface ITimeSlotRepository
    {
        public Task GenerateTimeSlotsAsync(List<TimeSlots> timeSlots);
        public Task<IEnumerable<TimeSlots>> GetByCenterId(Guid centerId);
        public Task<TimeSlots> GetLastTimeSlot(Guid centerId);
    }
}
