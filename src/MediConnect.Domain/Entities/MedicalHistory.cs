using MediConnect.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Domain.Entities
{
	public class MedicalHistory : BaseEntity
	{
		public Guid PatientId { get; set; }
		public string ConditionName { get; set; } = string.Empty;
		public DateOnly DiagnosisDate { get; set; }
		public bool IsActive { get; set; } = true;
		public string? Notes { get; set; }
		//Navigation
		public Patient Patient { get; set; } = null!;
	}
}
