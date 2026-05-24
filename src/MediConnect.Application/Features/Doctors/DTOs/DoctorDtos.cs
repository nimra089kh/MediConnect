using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediConnect.Application.Features.Doctors.DTOs
{
	public class SetAvailabilityDto
	{
		public Guid DoctorId { get; set; }
		public DayOfWeek DayOfWeek { get; set; }
		public TimeOnly StartTime { get; set; }
		public TimeOnly EndTime { get; set; }
		public int SlotDurationMinutes { get; set; } = 30; // Default to 30 minutes
	}

	public class GenerateSlotsDto
	{
		public Guid DoctorId { get; set; }
	    public DateOnly FromDate { get; set; }
		public DateOnly ToDate { get; set; }

	}

	public class AvailableSlotDto
	{
		public Guid SlotId { get; set; }
		public DateOnly Date { get; set; }
		public TimeOnly StartTime { get; set; }
		public TimeOnly EndTime { get; set; }

	}
}
