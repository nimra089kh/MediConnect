using MediConnect.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Domain.Entities
{
	public class Department : BaseEntity
	{
		public Guid HospitalId { get; set; }
		public string Name { get; set; } = string.Empty;
		public bool IsActive { get; set; } = true;

		public Hospital Hospital { get; set; } = null!;
		public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

	}
}
