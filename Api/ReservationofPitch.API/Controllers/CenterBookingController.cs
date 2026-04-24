using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Reservationpitch.Application.Common.Handling;
using Reservationpitch.Application.DTOs.StadiumReservationDtos.Request;
using Reservationpitch.Application.DTOs.StadiumReservationDtos.Response;
using Reservationpitch.Application.Services.CenterBookingServices.Interfaces;
using Reservationpitch.Domain.Shared;

namespace ReservationofPitch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CenterBookingController : ControllerBase
    {
        private readonly ICenterBookingService _service;

        public CenterBookingController(ICenterBookingService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<Result<CenterBookingResponseDto>> CreateBooking([FromBody] CenterBookingDto bookingDto)
        {
            var result = await _service.CreateBookingAsync(bookingDto);

            return await Result<CenterBookingResponseDto>.SuccessAsync(result.Data, ResponseStatus.CreateSuccess, true);
        }

        [HttpGet]
        public async ValueTask<Result<IEnumerable<CenterBookingResponseDto>>> GetBookings()
        {
            var bookings = await _service.GetAllBookingsAsync();
            return await Result<IEnumerable<CenterBookingResponseDto>>.SuccessAsync(bookings.Data, ResponseStatus.GetAllSuccess, true);
        }

        [HttpGet("{id}")]
        public async Task<Result<CenterBookingResponseDto>> GetBookingById(Guid id)
        {
            var booking = await _service.GetBookingByIdAsync(id);
            
            return await Result<CenterBookingResponseDto>.SuccessAsync(booking.Data, booking.Message, true);
        }
    }
}
