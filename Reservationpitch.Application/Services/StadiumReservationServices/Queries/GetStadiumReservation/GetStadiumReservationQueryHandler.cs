using OnMapper;
using Reservationpitch.Application.Abstractions.Messaging;
using Reservationpitch.Application.Common.Handling;
using Reservationpitch.Application.DTOs.StadiumDTOs.Response;
using Reservationpitch.Domain.Entities;
using Reservationpitch.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.Services.StadiumReservationServices.Queries.GetStadiumReservation
{
    public class GetStadiumReservationQueryHandler : IQueryHandler<GetStadiumReservationQuery, IEnumerable<StadiumReservationResponseDto>>
    {
        private readonly IStadiumReservationRepository _repository;
        private readonly OnMapping _mapper;
        public GetStadiumReservationQueryHandler(IStadiumReservationRepository repository, OnMapping mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<StadiumReservationResponseDto>>> Handle(GetStadiumReservationQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync(cancellationToken);

            var response = await _mapper.MapCollection<StadiumReservation,  StadiumReservationResponseDto>(result);

            return await Result<IEnumerable<StadiumReservationResponseDto>>.SuccessAsync(response.Data, "Viewed Successfully", true);
        }
    }
}
