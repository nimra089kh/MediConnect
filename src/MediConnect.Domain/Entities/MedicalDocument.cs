using MediConnect.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Domain.Entities
{
	public class MedicalDocument : BaseEntity
	{
		public Guid PatientId { get; set; }
		public Guid? AppointmentId { get; set; }
		public string FilePath { get; set; } = string.Empty;
		public string FileName { get; set; } = string.Empty;
		public Guid UploadedBy { get; set; }
		public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
		//Navigation
		public Patient Patient { get; set; } = null!;
		public Appointment? Appointment { get; set; }
	}
}
