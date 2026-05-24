using MediConnect.Domain.Common;
using MediConnect.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Domain.Entities
{
	public class Patient : BaseEntity
	{
		public Guid UserId { get; set; }
		public Guid HospitalId { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public DateOnly DateOfBirth { get; set; }
		public Gender Gender {get; set; }
		public BloodGroup? BloodGroup { get; set; }
		public string? Phone { get; set; }
		public string? Address { get; set; }
		public string? EmergencyContact { get; set; }
		public bool IsActive { get; set; } = true;

		//computed properties
		public int Age => DateTime.Now.Year - DateOfBirth.Year;
		public string FullName => $"{FirstName} {LastName}";

		//Navigation properties
		public User User { get; set; } = null!;
		public Hospital Hospital { get; set; } = null!;
		public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
		public ICollection<MedicalHistory> MedicalHistories { get; set; } = new List<MedicalHistory>();
		public ICollection<MedicalDocument> MedicalDocuments { get; set; } = new List<MedicalDocument>();


	}
}
