using Reservationpitch.Application.Abstractions.Messaging;
using Reservationpitch.Application.Common.Handling;
using Reservationpitch.Domain.Entities;
using Reservationpitch.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservationpitch.Application.Services.TimeSlotService.Commands.CreateTimeSlot
{
    public class GenerateTimeSlotCommandHandler : ICommandHandler<GenerateTimeSlotCommand, bool>
    {
        private readonly ITimeSlotRepository _timeSlotRepository;
        private readonly IWorkDayRepository _workDayRepository;
        public GenerateTimeSlotCommandHandler(ITimeSlotRepository timeSlotRepository, IWorkDayRepository workDayRepository)
        {
            _timeSlotRepository = timeSlotRepository;
            _workDayRepository = workDayRepository;
        }

        public async Task<Result<bool>> Handle(GenerateTimeSlotCommand request, CancellationToken cancellationToken)
        {

            // To fetch all time info of a specific center
            var centerTimeInfo = await _workDayRepository.GetCenterWorkingDays(request.CenterId);
            // Get all days of that center
            var workingDayNames = centerTimeInfo.Select(w => w.DayOfWeek).ToList();

            var center = centerTimeInfo.FirstOrDefault();

            if (center == null)
            {
                return await Result<bool>.FaildAsync(false, "Center is null with this Id");
            }


            // Check if there is not start and end time for that center
            if (!center.BeginWorkTime.HasValue || !center.EndWorkTime.HasValue)
                return await Result<bool>.FaildAsync(false, $"Missing time settings. StartTime: {center.BeginWorkTime}, EndTime: {center.EndWorkTime}");


            // Initilaize a new list of time slots 
            var timeSlots = new List<TimeSlots>();

            // fetch all last time slots for that center with descending order
            var lasttimeslot = await _timeSlotRepository.GetLastTimeSlot(request.CenterId);

            //

            DateTime baseDate = lasttimeslot != null
                ? lasttimeslot.DateOfTimeSlot.ToDateTime(TimeOnly.MinValue).AddDays(1)
                : DateTime.Today;


            int daysToGenerate = centerTimeInfo.Count();
            int daysGenerated = 0;
            DateTime currentDate = baseDate;

            while (daysGenerated < daysToGenerate)
            {
                string currentDayName = currentDate.DayOfWeek.ToString();

                if (!string.Equals(currentDayName, "Friday", StringComparison.OrdinalIgnoreCase)
                    && workingDayNames.Any(wd => string.Equals(wd, currentDayName, StringComparison.OrdinalIgnoreCase)))
                {
                    // --- Generate Morning Slots ---
                    if (center.StartBreakingTime.HasValue && center.BeginWorkTime.Value < center.StartBreakingTime.Value)
                    {
                        TimeOnly currentTime = center.BeginWorkTime.Value;
                        while (currentTime < center.StartBreakingTime.Value)
                        {
                            timeSlots.Add(new TimeSlots
                            {
                                CenterId = request.CenterId,
                                // Using DateOnly.FromDateTime to store only the date part.
                                DateOfTimeSlot = DateOnly.FromDateTime(currentDate),
                                TimeSlotValue = currentTime,
                                TimeStatus = "Morning",
                                Status = false
                            });
                            //currentTime = currentTime.Add(interval);
                        }
                    }
                    else
                    {
                        // If no break time is provided, create slots for the full duration as morning slots.
                        TimeOnly currentTime = center.BeginWorkTime.Value;
                        while (currentTime < center.EndWorkTime.Value)
                        {
                            timeSlots.Add(new TimeSlots
                            {
                                CenterId = request.CenterId,
                                DateOfTimeSlot = DateOnly.FromDateTime(currentDate),
                                TimeSlotValue = currentTime,
                                TimeStatus = "Morning",
                                Status = false
                            });
                            //currentTime = currentTime.Add(interval);
                        }
                    }

                    // --- Generate Evening Slots (if applicable) ---
                    if (center.EndBreakingTime.HasValue && center.EndBreakingTime.Value < center.EndWorkTime.Value)
                    {
                        TimeOnly currentTime = center.EndBreakingTime.Value;
                        while (currentTime < center.EndWorkTime.Value)
                        {
                            timeSlots.Add(new TimeSlots
                            {
                                CenterId = request.CenterId,
                                DateOfTimeSlot = DateOnly.FromDateTime(currentDate),
                                TimeSlotValue = currentTime,
                                TimeStatus = "Evening",
                                Status = false
                            });
                            //currentTime = currentTime.Add(interval);
                        }
                    }

                    daysGenerated++;
                }

                currentDate = currentDate.AddDays(1);
            }
            await _timeSlotRepository.GenerateTimeSlotsAsync(timeSlots);

            return await Result<bool>.SuccessAsync(true, "Time slots are generated successfully for this center");
        }

        private static DateTime? GetNextDayOfWeek(string dayName)
        {
            if (!Enum.TryParse<DayOfWeek>(dayName, true, out var targetDay))
                return null;

            var today = DateTime.Today;
            int daysToAdd = ((int)targetDay - (int)today.DayOfWeek + 7) % 7;
            return today.AddDays(daysToAdd);
        }
    }
}
