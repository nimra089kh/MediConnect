using MediConnect.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Domain.Entities
{
	public class Specialization: BaseEntity
	{
		public string Name { get; set; } = string.Empty;
		public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
	}
}
