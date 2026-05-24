using MediConnect.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Domain.Entities
{
	public class AppointmentSlot: BaseEntity
	{
		public Guid DoctorId { get; set; }
		public Guid HospitalId { get; set; }
		public DateOnly SlotDate { get; set; }
		public TimeOnly StartTime { get; set; }
		public TimeOnly EndTime { get; set; }
		public bool IsBooked { get; set; } = false;


		// Navigation
		public Doctor Doctor { get; set; } = null!;
		public Hospital Hospital { get; set; } = null!;

	}
}
