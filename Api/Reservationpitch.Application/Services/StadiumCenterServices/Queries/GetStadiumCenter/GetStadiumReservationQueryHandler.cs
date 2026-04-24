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

namespace Reservationpitch.Application.Services.StadiumCenterServices.Queries.CreateStadiumCenter
{
    public class GetStadiumCenterQueryHandler : IQueryHandler<GetStadiumCenterQuery, IEnumerable<StadiumCenterResponseDto>>
    {
        private readonly IStadiumCenterRepository _repository;
        private readonly OnMapping _mapper;
        public GetStadiumCenterQueryHandler(IStadiumCenterRepository repository, OnMapping mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<StadiumCenterResponseDto>>> Handle(GetStadiumCenterQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync(cancellationToken);

            var response = await _mapper.MapCollection<StadiumCenter, StadiumCenterResponseDto>(result);

            return await Result<IEnumerable<StadiumCenterResponseDto>>.SuccessAsync(response.Data, "Viewed Successfully", true);
        }
    }
}
