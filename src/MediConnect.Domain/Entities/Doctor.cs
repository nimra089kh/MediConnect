using MediConnect.Domain.Common;
using MediConnect.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Domain.Entities
{
	public class Doctor : BaseEntity
	{
		public Guid UserId { get; set; }
		public Guid HospitalId { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; }= string.Empty;
		public string LicenseNumber { get; set; } = string.Empty;
		public decimal ConsultationFee { get; set; }
		public string? Bio { get; set; }
		public string? Phone { get; set; }
		public Gender Gender { get; set; }
		public bool IsActive { get; set; } = true;

		public string FullName => $"{FirstName} {LastName}";

		//Navigation properties
		public User User { get; set; } = null!;
		public Hospital Hospital { get; set; } = null!;
		public ICollection<DoctorAvailability> Availabilities { get; set; } = new List<DoctorAvailability>();
		public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
		public ICollection<Department> Departments { get; set; } = new List<Department>();
		public ICollection<Specialization> Specializations { get; set; } = new List<Specialization>();



	}
}
