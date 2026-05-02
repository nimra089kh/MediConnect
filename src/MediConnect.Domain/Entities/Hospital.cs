using MediConnect.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Domain.Entities
{
	public class Hospital : BaseEntity
	{
		public string Name { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public string Phone { get; set; } = string.Empty;
		public bool IsActive { get; set; } = true;

       //Navigation properties
	   public ICollection<Department> Departments { get; set; } = new List<Department>();
	   public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
	   public ICollection<Patient> Patients { get; set; } = new List<Patient>();
	}
}
