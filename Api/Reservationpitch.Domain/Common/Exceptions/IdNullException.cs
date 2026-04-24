using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Domain.Common.Exceptions
{
    public class IdNullException: SystemException
    {
        public IdNullException(string? message):base(message) {
        
        }
        
    }
}
