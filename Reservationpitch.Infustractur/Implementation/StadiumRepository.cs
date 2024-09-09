using Microsoft.EntityFrameworkCore;
using Reservationpitch.Application.Common.Handling;
using Reservationpitch.Domain.Entities;
using Reservationpitch.Domain.Interfaces;
using Reservationpitch.Infustractur.Data;
using Reservationpitch.Infustractur.Database;

namespace Reservationpitch.Infustractur.Implementation
{
    public class StadiumRepository : GenericRepository<Stadium>,IStadiumRepository
    {
        public StadiumRepository(ApplicationDbContext context):base(context)
        {
            
        }

        public async Task<IEnumerable<Stadium>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _context.Stadiums.ToListAsync();
            if(result.Count > 0)
            {
                return result;
            }
            return Enumerable.Empty<Stadium>();
        }

        public async Task<IEnumerable<Stadium>>GetAllStadiumByCenterId(Guid centerId, CancellationToken cancellationToken)
        {
            var result = await _context.Stadiums.Where(s=> s.stadiumCenterId == centerId).ToListAsync();

            if(result.Count > 0)
            {
                return result;
            }
            return Enumerable.Empty<Stadium>();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Stadium> UpdateAsync(Stadium entity, CancellationToken cancellationToken)
        {
            var findId = await _context.Stadiums.FirstOrDefaultAsync(c => c.Id == entity.Id);
            if (findId != null)
            {
                // Update the blogpost
                _context.Entry(findId).CurrentValues.SetValues(entity);
               
                await _context.SaveChangesAsync();
                return entity;
            }
            else
            {
                return null;
            }
        }
    }
}
