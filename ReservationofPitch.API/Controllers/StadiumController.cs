using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reservationpitch.Application.Common.Handling;
using Reservationpitch.Application.DTOs.StadiumDTOs.Request;
using Reservationpitch.Application.DTOs.StadiumDTOs.Response;
using Reservationpitch.Application.Services.StadiumServices.Commands.UpdateStadiumService;
using Reservationpitch.Application.Services.StadiumServices.Queries.GetStadiumService;
using Reservationpitch.Application.Services.StadiumServices.Queries.GetStadiumByIdService;
using Reservationpitch.Application.Services.StadiumServices.Commands.AddStadiumService;
using Reservationpitch.Application.Services.StadiumServices.Queries.GetStadiumByCenterId;

namespace ReservationofPitch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StadiumController : ControllerBase
    {
        private readonly ISender _sender;
        public StadiumController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("getStadium")]
        public async Task<Result<IEnumerable<StadiumResponseDto>>> Get(CancellationToken cancellationToken)
        {
            var query = new GetStadiumQuery();

            var response = await _sender.Send(query, cancellationToken);
            if (response.IsSuccess)
            {
                return await Result<IEnumerable<StadiumResponseDto>>.SuccessAsync(response.Data, "Viewed successfully", true);
            }

            return await Result<IEnumerable<StadiumResponseDto>>.FaildAsync(false, "Faild in fetching data");
        }


        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<Result<StadiumResponseDto>> Get([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var query = new GetByIdStadiumQuery
            {
                Id = id,
            };

            var response = await _sender.Send(query, cancellationToken);

            if (response.IsSuccess)
            {
                return await Result<StadiumResponseDto>.SuccessAsync(response.Data, "Found Successfully", true);
            }

            return await Result<StadiumResponseDto>.FaildAsync(false, "Not found");
        }


        //[HttpGet("GetAllUserMassageRciver/{receiverId}")]

        [HttpGet("getByCenter/{centerId}")]
        public async Task<Result<IEnumerable<StadiumResponseDto>>> GetCenter(Guid centerId, CancellationToken cancellationToken)
        {
            var query = new GetStadiumByCenterIdQuery
            {
                CenterId = centerId
            };

            var response = await _sender.Send(query, cancellationToken);

            if(response.IsSuccess)
            {
                return await Result<IEnumerable<StadiumResponseDto>>.SuccessAsync(response.Data, "Get All Successfully", true);
            }


            return await Result<IEnumerable<StadiumResponseDto>>.FaildAsync(false, "Get All Successfully");
        }

        [HttpPost]
        public async Task<Result<StadiumResponseDto>> Post([FromBody] StadiumRequestDto request, CancellationToken cancellationToken)
        {

            var command = new CreateStadiumCommand()
            {
                Name = request.Name,
                NoOfplayers = request.NoOfplayers,
                PriceinHour =   request.PriceinHour,
                stadiumCenterId = request.stadiumCenterId,
                Type = request.Type
            };

            //Mapping the request to the command


            var response = await _sender.Send(command, cancellationToken);

            if (response.IsSuccess)
            {
                return await Result<StadiumResponseDto>.SuccessAsync(response.Data, "Created Successfully", true);
            }

            return await Result<StadiumResponseDto>.FaildAsync(false, "Not created");
        }


        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<Result<StadiumResponseDto>> Put([FromRoute] Guid id, [FromBody] StadiumRequestDto request, CancellationToken cancellationToken)
        {

            var command = new UpdateStadiumCommand()
            {
                Name = request.Name,
                NoOfplayers = request.NoOfplayers,
                PriceinHour = request.PriceinHour,
                stadiumCenterId = request.stadiumCenterId,
                Type = request.Type
            };

            command.Id = id;

            var response = await _sender.Send(command, cancellationToken);

            if (response.IsSuccess)
            {

                return await Result<StadiumResponseDto>.SuccessAsync(response.Data, "Updated Successfully", true);
            }


            return await Result<StadiumResponseDto>.FaildAsync(false, "Falid in updated this record");
        }



        //[HttpDelete]
        //[Route("{id:Guid}")]
        //public async Task<Result<bool>> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        //{
        //    var command = new DeletePitchCommand
        //    {
        //        Id = id
        //    };

        //    var response = await _sender.Send(command, cancellationToken);

        //    if (response.IsSuccess)
        //    {
        //        return await Result<bool>.SuccessAsync(response.Data, "Deleted Successfully", true);
        //    }
        //    return await Result<bool>.FaildAsync(false, "Falid in deleted this record");

        //}

    }
}
