using OnMapper;
using Reservationpitch.Application.Abstractions.Messaging;
using Reservationpitch.Application.Common.Handling;
using Reservationpitch.Application.DTOs.StadiumDTOs.Response;
using Reservationpitch.Application.Services.StadiumReservationServices.Queries.GetStadiumReservation;
using Reservationpitch.Domain.Entities;
using Reservationpitch.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.Services.StadiumReservationServices.Queries.GetStadiumReservationById
{
    public class GetStadiumReservationByIdQueryHandler : IQueryHandler<GetStadiumReservationByIdQuery, StadiumReservationResponseDto>
    {
        private readonly IStadiumReservationRepository _repository;
        private readonly OnMapping _mapper;
        public GetStadiumReservationByIdQueryHandler(IStadiumReservationRepository repository, OnMapping mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<Result<StadiumReservationResponseDto>> Handle(GetStadiumReservationByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByIdAsync(request.Id, cancellationToken);

            var response = await _mapper.Map<StadiumReservation, StadiumReservationResponseDto>(result);

            return await Result<StadiumReservationResponseDto>.SuccessAsync(response.Data, "Viewed Successfully", true);
        }
    }
}
