using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Domain.Common.Exceptions
{
    public class ModelNullException:ArgumentNullException
    {
        public ModelNullException(string? paramName, string? message) :base(paramName, message)
        {
                        
        }
    }
}
