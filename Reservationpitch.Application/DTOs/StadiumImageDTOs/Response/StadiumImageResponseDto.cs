using Reservationpitch.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.DTOs.StadiumImageDTOs.Response
{
    public class StadiumImageResponseDto
    {
        public string FileName { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public Guid StadiumId { get; set; }

        public StadiumImageResponseDto FromModel(StadiumImages model)
        {
            return new StadiumImageResponseDto
            {
                FileName = model.FileName,
                ImageUrl = model.ImageUrl,
                CreatedOn = model.CreatedOn,
                StadiumId = model.StadiumId,

            };
        }

        public List<StadiumImageResponseDto> FromModel(IEnumerable<StadiumImages> model)
        {

            List<StadiumImageResponseDto> responses = new List<StadiumImageResponseDto>();
            foreach (var item in model)
            {
                var res = new StadiumImageResponseDto
                {
                    FileName = item.FileName,
                    ImageUrl = item.ImageUrl,
                    CreatedOn = item.CreatedOn,
                    StadiumId = item.StadiumId,
                };
                responses.Add(res);
            }
            var list = new List<StadiumImageResponseDto>();
            list.AddRange(model.Select((x) => FromModel(x)));
            return list;

        }
    }
}
