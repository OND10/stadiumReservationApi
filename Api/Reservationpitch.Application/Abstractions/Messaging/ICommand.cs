using MediatR;
using Reservationpitch.Application.Common.Handling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.Abstractions.Messaging
{
    public interface ICommand<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
