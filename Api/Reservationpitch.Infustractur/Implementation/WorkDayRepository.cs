using Microsoft.EntityFrameworkCore;
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
    public class WorkDayRepository : IWorkDayRepository
    {
        private readonly ApplicationDbContext _context;
        public WorkDayRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<WorkDays>> GetCenterWorkingDays(Guid centerId)
        {
            var result = await _context.WorkDays.Where(w => w.stadiumCenterId == centerId).ToListAsync();

            if (result.Count() > 0)
            {
                return result;
            }

            return Enumerable.Empty<WorkDays>();
        }
    }
}

