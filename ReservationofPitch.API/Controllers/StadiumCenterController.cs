using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reservationpitch.Application.Common.Handling;
using Reservationpitch.Application.DTOs.StadiumDTOs.Request;
using Reservationpitch.Application.DTOs.StadiumDTOs.Response;
using Reservationpitch.Application.Services.StadiumCenterServices.Commands.CreateStadiumCenter;
using Reservationpitch.Application.Services.StadiumCenterServices.Queries.CreateStadiumCenter;
using Reservationpitch.Application.Services.StadiumCenterServices.Queries.CreateStadiumCenterById;

namespace ReservationofPitch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StadiumCenterController : ControllerBase
    {
        private readonly ISender _sender;
        public StadiumCenterController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("getCenter")]
        public async Task<Result<IEnumerable<StadiumCenterResponseDto>>> Get(CancellationToken cancellationToken)
        {
            var query = new GetStadiumCenterQuery();

            var response = await _sender.Send(query, cancellationToken);
            if (response.IsSuccess)
            {
                return await Result<IEnumerable<StadiumCenterResponseDto>>.SuccessAsync(response.Data, "Viewed successfully", true);
            }

            return await Result<IEnumerable<StadiumCenterResponseDto>>.FaildAsync(false, "Faild in fetching data");
        }


        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<Result<StadiumCenterResponseDto>> Get([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var query = new GetStadiumCenterByIdQuery
            {
                Id = id,
            };

            var response = await _sender.Send(query, cancellationToken);

            if (response.IsSuccess)
            {
                return await Result<StadiumCenterResponseDto>.SuccessAsync(response.Data, "Found Successfully", true);
            }

            return await Result<StadiumCenterResponseDto>.FaildAsync(false, "Not found");
        }


        [HttpPost]
        public async Task<Result<StadiumCenterResponseDto>> Post([FromForm] StadiumCenterRequestDto request, CancellationToken cancellationToken)
        {

            var command = new CreateStadiumCenterCommand()
            {
                DressingRoomVisible = request.DressingRoomVisible,
                Location = request.Location,
                Name = request.Name,
                Owned_By = request.Owned_By,
                PhoneNumber = request.PhoneNumber,
                file = request.file
            };

            //Mapping the request to the command


            var response = await _sender.Send(command, cancellationToken);

            if (response.IsSuccess)
            {
                return await Result<StadiumCenterResponseDto>.SuccessAsync(response.Data, "Created Successfully", true);
            }

            return await Result<StadiumCenterResponseDto>.FaildAsync(false, "Not created");
        }


  
    }
}
