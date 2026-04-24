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

namespace Reservationpitch.Application.Services.StadiumServices.Queries.GetStadiumByCenterId
{
    public class GetStadiumByCenterIdQueryHandler : IQueryHandler<GetStadiumByCenterIdQuery, IEnumerable<StadiumResponseDto>>
    {

        private readonly IStadiumRepository _repository;
        private readonly OnMapping _mapper;
        public GetStadiumByCenterIdQueryHandler(IStadiumRepository repository, OnMapping mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<StadiumResponseDto>>> Handle(GetStadiumByCenterIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllStadiumByCenterId(request.CenterId, cancellationToken);

            var mappedModel = await _mapper.MapCollection<Stadium, StadiumResponseDto>(result);

            return await Result<IEnumerable<StadiumResponseDto>>.SuccessAsync(mappedModel.Data, "Data retrieved Successfully", true);
        }

    }
}
