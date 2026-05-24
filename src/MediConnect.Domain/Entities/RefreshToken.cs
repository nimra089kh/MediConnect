using MediConnect.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Domain.Entities
{
	public class RefreshToken : BaseEntity
	{
		public Guid UserId { get; set; }
		public string Token { get; set; } = string.Empty;
		public DateTime ExpiresAt { get; set; }
		public bool IsRevoked { get; set; } = false;

		// Computed — is this token still valid?
		public bool IsActive => !IsRevoked && DateTime.UtcNow < ExpiresAt;

		public User User { get; set; } = null!;

	}
}
