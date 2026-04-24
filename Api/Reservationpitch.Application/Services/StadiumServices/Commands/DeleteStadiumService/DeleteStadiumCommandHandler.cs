using Reservationpitch.Application.Abstractions.Messaging;
using Reservationpitch.Application.Common.Handling;
using Reservationpitch.Domain.Interfaces;

namespace Reservationpitch.Application.Services.StadiumServices.Commands.DeleteStadiumService
{
    public class DeleteStadiumCommandHandler : ICommandHandler<DeleteStadiumCommand, bool>
    {
        private readonly IStadiumRepository _pitchRepository;
        public DeleteStadiumCommandHandler(IStadiumRepository pitchRepository)
        {
            _pitchRepository = pitchRepository;
        }
        public async Task<Result<bool>> Handle(DeleteStadiumCommand request, CancellationToken cancellationToken)
        {
            await _pitchRepository.DeleteAsync(request.Id, cancellationToken);

            return await Result<bool>.SuccessAsync("Deleted Successfully", true);
        }
    }
}
