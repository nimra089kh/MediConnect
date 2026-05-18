using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Application.Features.Appointments.DTOs
{
	public class AppointmentResponseDto
	{
		public Guid Id { get; set; }
		public string DoctorFullName { get; set; } = string.Empty;
		public string PatientFullName { get; set; } = string.Empty;
		public DateOnly AppointmentDate { get; set; }
		public TimeOnly StartTime { get; set; }
		public TimeOnly EndTime { get; set; }
		public decimal ConsultationFees { get; set; }
		public string? Notes { get; set; }
		public string Status { get; set; } = string.Empty;
	}
	public class BookAppointmentDto
	{
		public Guid DoctorId { get; set; }
		public Guid SlotId { get; set; }
		public DateOnly AppointmentDate { get; set; }
		public string? Notes { get; set; }
	}
}
