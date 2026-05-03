using MediConnect.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Domain.Entities
{
	public class Notification: BaseEntity
	{
		public Guid HospitalId { get; set; }
		public Guid UserId { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Messege { get; set; } = string.Empty;
		public bool IsRead { get; set; } = false;
		public DateTime? ReadAt { get; set; } 

		public Hospital Hospital { get; set; } = null!;
		public User User { get; set; } = null!;
	}
}
