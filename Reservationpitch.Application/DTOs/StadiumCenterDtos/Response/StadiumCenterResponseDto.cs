using Reservationpitch.Application.Common.Mappings.PitchMapper;
using Reservationpitch.Domain.Entities;

namespace Reservationpitch.Application.DTOs.StadiumDTOs.Response
{
    public class StadiumCenterResponseDto : IMap<StadiumCenter>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public string Owned_By { get; set; } = string.Empty;
        public bool DressingRoomVisible { get; set; } = false;
        public string ImageUrl { get; set; }

        public StadiumCenterResponseDto FromModel(StadiumCenter model)
        {
            return new StadiumCenterResponseDto
            {
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                Location = model.Location,
                DressingRoomVisible = model.DressingRoomVisible,
                Owned_By = model.Owned_By,
                Id = model.Id,
                ImageUrl = model.ImageUrl,
            };
        }
    }
}
