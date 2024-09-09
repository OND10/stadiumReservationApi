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
        public async Task<StadiumReservation> CreateAsync(StadiumReservation entity, CancellationToken cancellationToken)
        {
            try
            {
                await _context.StadiumReservations.AddAsync(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception)
            {
                throw new ModelNullException($"{entity}", " Exception : StadiumReservation is null");
            }
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var StadiumReservation = await _context.StadiumReservations.Where(s => s.Id == id).FirstOrDefaultAsync();

            if (StadiumReservation is not null)
            {
                _context.StadiumReservations.Remove(StadiumReservation);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;

        }

        public async Task<IEnumerable<StadiumReservation>> GetAllAsync(CancellationToken cancellationToken)
        {
            var list = await _context.StadiumReservations.ToListAsync();

            if (list.Count > 0)
            {
                return list;
            }

            return Enumerable.Empty<StadiumReservation>();
        }

        public async Task<StadiumReservation> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _context.StadiumReservations.FindAsync(id);

            if (result is not null)
            {
                return result;
            }

            throw new IdNullException("Id is null");
        }

        public async Task<StadiumReservation> UpdateAsync(StadiumReservation entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }
}
