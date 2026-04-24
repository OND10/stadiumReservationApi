using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.Shared
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> WhereNot<T>(this IEnumerable<T> source, Func<T, bool> predicate) { 
            return source.Where(item => !predicate(item));
        }

        public static IEnumerable<IEnumerable<T>> Chunck<T>(this IEnumerable<T> source, int size)
        {
            if (size <= 0)
                throw new ArgumentException("Chunck size most be greater than 0");

            return source.Select((item, index) => new {item, index})
                         .GroupBy(x => x.index/size)
                         .Select(group => group.Select(x => x.item));
        }
    }
}
