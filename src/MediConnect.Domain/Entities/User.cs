using MediConnect.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Domain.Entities
{
	public class User : BaseEntity
	{
		public Guid HospitalId{ get; set; }
		public string Email { get; set; } = string.Empty;
		public string PasswordHash { get; set; } = string.Empty;
		public bool IsActive { get; set; } = true;

		//Navigation properties
		public Hospital Hospital { get; set; } = null!;
		public Doctor? Doctor { get; set; }
		public Patient? Patient { get; set; }
		public ICollection<RefreshToken>  RefreshTokens { get; set; } = new List<RefreshToken>();
	}
}
