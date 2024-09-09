using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Reservationpitch.Application.DTOs.WorkDayDTOs.Request;
using Reservationpitch.Domain.Entities;
using Reservationpitch.Infustractur.Database;

namespace ReservationofPitch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkDayController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public WorkDayController(ApplicationDbContext context) 
        { 
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WorkDayRequestDto request)
        {
            var model = new WorkDays
            {
                stadiumCenterId = request.stadiumCenterId,
                BeginWorkTime = request.BeginWorkTime,
                DayOfWeek = request.DayOfWeek.ToString(),
                EndWorkTime = request.EndWorkTime,
            };

            var result = await _context.WorkDays.AddAsync(model);
            
            
            if(result.State == Microsoft.EntityFrameworkCore.EntityState.Added)
            {
                await _context.SaveChangesAsync();
                return Ok();
            }

            return BadRequest();
        }
    }
}
