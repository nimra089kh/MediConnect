using MediConnect.Domain.Common;
using MediConnect.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Domain.Entities
{
	public class Payment : BaseEntity
	{
		public Guid AppointmentId { get; set; }
		public Guid PatientId { get; set; }
		public decimal Amount { get; set; }
		public string? StripePaymentId { get; set; } = string.Empty;
		public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
		public string? ReciptURL { get; set; } = string.Empty;
		public DateTime? PaidAt { get; set; }

		// Navigation
		public Appointment Appointment { get; set; } = null!;
		public Patient Patient { get; set; } = null!;
		public Refund? Refund { get; set; }

	}
}
