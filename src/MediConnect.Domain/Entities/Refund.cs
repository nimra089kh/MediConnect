using MediConnect.Domain.Common;
using MediConnect.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Domain.Entities
{
	public class Refund : BaseEntity
	{
		public Guid PaymentId { get; set; }
		public decimal Amount { get; set; }
		public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

		public string? StripeRefundId { get; set; } = string.Empty;
		public string? Reason { get; set; } = string.Empty;
		public DateTime? ProcessedAt { get; set; }
		// Navigation
		public Payment Payment { get; set; } = null!;
	}
}
