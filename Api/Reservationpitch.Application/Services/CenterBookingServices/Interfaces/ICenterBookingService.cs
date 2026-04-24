using Reservationpitch.Application.Common.Handling;
using Reservationpitch.Application.DTOs.StadiumReservationDtos.Request;
using Reservationpitch.Application.DTOs.StadiumReservationDtos.Response;
using Reservationpitch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.Services.CenterBookingServices.Interfaces
{
    public interface ICenterBookingService
    {
        Task<Result<CenterBookingResponseDto>> CreateBookingAsync(CenterBookingDto bookingDto);
        Task<Result<IEnumerable<CenterBookingResponseDto>>> GetAllBookingsAsync();
        Task<Result<CenterBookingResponseDto?>> GetBookingByIdAsync(Guid id);
    }
}
