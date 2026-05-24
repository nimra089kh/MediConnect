using MediConnect.Domain.Common;
using MediConnect.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Domain.Entities
{
	public class AppointmentStatusHistory:BaseEntity
	{
		public Guid AppointmentId { get; set; }
		public AppointmentStatus OldStatus { get; set; }
		public AppointmentStatus NewStatus { get; set; }
		public Guid ChangedBy { get; set; }
		public string? Reason { get; set; }

		// Navigation
		public Appointment Appointment { get; set; } = null!;

	}
}
