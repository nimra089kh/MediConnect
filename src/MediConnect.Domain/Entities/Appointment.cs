using MediConnect.Domain.Common;
using MediConnect.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Domain.Entities
{
	public class Appointment : BaseEntity
	{
		public Guid HosptalId { get; set; }
		public Guid PatientId { get; set; }
		public Guid DoctorId { get; set; }
		public Guid SlotId { get; set; }
		public DateOnly AppointmentDate { get; set; }
		public TimeOnly StartTime { get; set; }
		public TimeOnly EndTime { get; set; }
		public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
		public string? Notes { get; set; }


		//Cancellation data
		public Guid? CancelledBy { get; set; }
		public string? CancellationReason { get; set; }
		public DateTime? CancelledAt { get; set; }


		//Rescedule tracking
		public Guid? RescheduledFromId { get; set; }


		//Navigation properties
		public Hospital Hospital { get; set; } = null!;
		public Patient Patient { get; set; } = null!;
		public Doctor Doctor { get; set; } = null!;
		public AppointmentSlot Slot { get; set; } = null!;
		public Appointment? RescheduledFrom { get; set; }
		public Payment? Payment { get; set; }
		public ICollection<AppointmentStatusHistory> StatusHistory { get; set; } = new List<AppointmentStatusHistory>();




	}
}
