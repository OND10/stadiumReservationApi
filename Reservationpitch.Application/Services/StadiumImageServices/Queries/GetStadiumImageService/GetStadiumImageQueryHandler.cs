using AutoMapper;
using Reservationpitch.Application.Abstractions.Messaging;
using Reservationpitch.Application.Common.Handling;
using Reservationpitch.Application.DTOs.StadiumImageDTOs.Response;
using Reservationpitch.Domain.Entities;
using Reservationpitch.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.Services.StadiumImageServices.Queries.GetStadiumImageService
{
    public class GetStadiumImageQueryHandler : ICommandHandler<GetStadiumImageQuery, IEnumerable<StadiumImageResponseDto>>
    {
        private readonly IStadiumImageRepository _repository;
        private readonly IMapper _mapper;
        public GetStadiumImageQueryHandler(IStadiumImageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<StadiumImageResponseDto>>> Handle(GetStadiumImageQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync(cancellationToken);

            var response = new List<StadiumImageResponseDto>();
            foreach (var item in result)
            {
                var responseList = new StadiumImageResponseDto
                {
                    CreatedOn = item.CreatedOn,
                    FileName = item.FileName,
                    ImageUrl = item.ImageUrl,
                    StadiumId = item.StadiumId,
                };
                response.Add(responseList);
            }
            
            return await Result<IEnumerable<StadiumImageResponseDto>>.SuccessAsync(response, "Viewed Successfully", true);

        }
    }
}
