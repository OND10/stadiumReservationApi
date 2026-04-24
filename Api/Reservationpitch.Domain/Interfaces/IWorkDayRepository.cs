using Reservationpitch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Domain.Interfaces
{
    public interface IWorkDayRepository
    {
        public Task<IEnumerable<WorkDays>> GetCenterWorkingDays(Guid centerId);

    }
}
