using Dapper;
using Microsoft.EntityFrameworkCore;
using Reservationpitch.Application.Common.Handling;
using Reservationpitch.Domain.Common.Exceptions;
using Reservationpitch.Domain.Interfaces;
using Reservationpitch.Infustractur.Data;
using Reservationpitch.Infustractur.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Infustractur.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        //private readonly DapperDbContext _db;
        public ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context)
        {
            //_db = db;
            _context = context;
        }

        public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken)
        {
            var addResult = await _context.AddAsync(entity, cancellationToken);
            if(addResult.State == Microsoft.EntityFrameworkCore.EntityState.Added)
            {
                await _context.SaveChangesAsync();
                return await Task.FromResult<T>(addResult.Entity);
            }

            else
            {
                throw new ModelNullException(nameof(entity), "Model is null");
            }
        }

        public async Task<T> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var findIdResult = await _context.Set<T>().FindAsync(id);
            if (findIdResult is null)
            {
                throw new IdNullException("Id is null");
            }
            else
            {
                var delete = _context.Remove(findIdResult);
                await _context.SaveChangesAsync();
                return await Task.FromResult<T>(delete.Entity);
            }
        }

        public Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
        {
            var result = await _context.Set<T>().Where(expression).FirstOrDefaultAsync();

            if(result is not null)
            {
                return result;
            }
            else
            {
                throw new IdNullException($"{expression}");
            }
        }

        public Task<IEnumerable<R>> FindAsync<R>(Expression<Func<T, R>> selector, Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<R>> GetAllAsync<R>(Expression<Func<T, R>> selector, CancellationToken cancellationToken)
        {
            //string query = $"Select * From ";
            //using(var connection = _db.CreateConnection())
            //{
            //    var pitchList = await connection.QueryAsync<R>(query);
            //    return pitchList.ToList();
            //}
            try
            {
                var listResult = await _context.Set<T>().Select(selector).ToListAsync();
                if (listResult.Count < 0)
                {
                    return await Task.FromResult<IEnumerable<R>>(listResult);
                }
                else
                {
                    throw new ModelNullException(nameof(listResult), "List is empty");
                }
            }
            catch(Exception ex)
            {

                throw new ModelNullException(nameof(ex), $"{ex.Message}");
            }
        }

        

        public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var findIdResult = _context.Set<T>().FindAsync(id);
            if (findIdResult.Result is null)
            {
                throw new IdNullException("Id is null");
            }
            else
            {
                return await Task.FromResult<T>(findIdResult.Result);
            }
        }

        public Task<IEnumerable<T>> QueryAsync(string query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
