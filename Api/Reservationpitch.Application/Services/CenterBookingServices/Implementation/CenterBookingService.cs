using AutoMapper;
using Reservationpitch.Application.Common.Handling;
using Reservationpitch.Application.DTOs.StadiumReservationDtos.Request;
using Reservationpitch.Application.DTOs.StadiumReservationDtos.Response;
using Reservationpitch.Application.Services.CenterBookingServices.Interfaces;
using Reservationpitch.Domain.Entities;
using Reservationpitch.Domain.Interfaces;
using Reservationpitch.Domain.Shared;

public class CenterBookingService : ICenterBookingService
{
    private readonly IStadiumReservationRepository _repository;
    private readonly IMapper _mapper; // Use AutoMapper for easier mapping.

    public CenterBookingService(IStadiumReservationRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<CenterBookingResponseDto>> CreateBookingAsync(CenterBookingDto bookingDto)
    {
        // Map DTO to database entity
        var bookingEntity = _mapper.Map<CenterBooking>(bookingDto);

        // Calculate total price
        var duration = (bookingDto.EndTime - bookingDto.BeginTime).TotalHours;
        bookingEntity.TotalPrice = duration * 100; // Example hourly price, replace with actual logic.

        // Save booking
        var createdBooking = await _repository.CreateBookingAsync(bookingEntity);

        // Map the saved entity to a response DTO
        var mappedDto = _mapper.Map<CenterBookingResponseDto>(createdBooking);

        return await Result<CenterBookingResponseDto>.SuccessAsync(mappedDto, ResponseStatus.CreateSuccess, true);
    }

    public async Task<Result<IEnumerable<CenterBookingResponseDto>>> GetAllBookingsAsync()
    {
        var bookings = await _repository.GetBookingsAsync();

        //mapping data from db model to Dto
        var mappedDto = _mapper.Map<IEnumerable<CenterBookingResponseDto>>(bookings);

        return await Result<IEnumerable<CenterBookingResponseDto>>.SuccessAsync(mappedDto, ResponseStatus.GetAllSuccess, true);
    }

    public async Task<Result<CenterBookingResponseDto?>> GetBookingByIdAsync(Guid id)
    {
        var booking = await _repository.GetBookingByIdAsync(id);

        if(booking is null)
        {
            return await Result<CenterBookingResponseDto>.FaildAsync(false, ResponseStatus.Faild);
        }

        var mappedDto = _mapper.Map<CenterBookingResponseDto>(booking);

        return await Result<CenterBookingResponseDto>.SuccessAsync(mappedDto, ResponseStatus.GetSuccess, true);
    }
}
