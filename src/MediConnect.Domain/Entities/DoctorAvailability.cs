using MediConnect.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Domain.Entities
{
	public class DoctorAvailability: BaseEntity
	{
		public Guid DoctorId { get; set; }
		public DayOfWeek DayOfWeek { get; set; }
		public TimeOnly StartTime { get; set; }
		public TimeOnly EndTime { get; set; }
		public int SlotDurationMinutes { get; set; } = 30;
		public bool IsAvailable { get; set; } = true;


		//Navigation
		public Doctor Doctor { get; set; } = null!;

	}
}
