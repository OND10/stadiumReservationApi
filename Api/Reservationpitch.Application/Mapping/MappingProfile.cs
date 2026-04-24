using AutoMapper;
using Reservationpitch.Application.DTOs.StadiumReservationDtos.Request;
using Reservationpitch.Application.DTOs.StadiumReservationDtos.Response;
using Reservationpitch.Domain.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Map DTOs to entities and vice versa
        CreateMap<CenterBookingDto, CenterBooking>();
        CreateMap<CenterBooking, CenterBookingResponseDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName)) // Example for nested mapping
            .ForMember(dest => dest.CenterName, opt => opt.MapFrom(src => src.StadiumCenter.Name));
    }
}
