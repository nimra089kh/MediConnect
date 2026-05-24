using MediConnect.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Domain.Entities
{
	public class AuditLog: BaseEntity
	{
		public Guid HospitalId { get; set; }
		public Guid UserId { get; set; }
		public string Action { get; set; } = string.Empty;
		public string EntityName { get; set; } = string.Empty;
		public Guid EntityId { get; set; }
		public string? OldValue { get; set; }
		public string? NewValue { get; set; }
		public string? IpAddress { get; set; }

		public Hospital Hospital { get; set; } = null!;
	}
}
