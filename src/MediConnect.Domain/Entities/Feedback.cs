using MediConnect.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Domain.Entities
{
	public class Feedback : BaseEntity
	{
		public Guid PatientId { get; set; }
		public Guid AppointmentId { get; set; }
		public Guid DoctorId { get; set; }

		public int Rating { get; set; } // 1 to 5
		public string? Comment { get; set; } 
		
		// Navigation properties
		public Patient Patient { get; set; } = null!;
		public Appointment Appointment { get; set; } = null!;
		public Doctor Doctor { get; set; } = null!;
	}
}
