using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Domain.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<R>> GetAllAsync<R>(Expression<Func<T, R>> selector, CancellationToken cancellationToken);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);
        Task<T> FindAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);
        Task<IEnumerable<R>> FindAsync<R>(Expression<Func<T, R>> selector, Expression<Func<T, bool>> expression);
        Task<T>CreateAsync(T entity, CancellationToken cancellationToken);
        Task<T>UpdateAsync(T entity, CancellationToken cancellationToken);
        Task<T> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<T>> QueryAsync(string query, CancellationToken cancellationToken);


    }
}
