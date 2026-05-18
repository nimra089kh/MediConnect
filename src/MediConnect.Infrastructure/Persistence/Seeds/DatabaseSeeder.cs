using MediConnect.Domain.Entities;
using MediConnect.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Infrastructure.Persistence.Seeds
{
	public static class DatabaseSeeder
	{
		public static async Task SeedAsync(ApplicationDbContext context)
		{
			if(await context.Hospitals.AnyAsync())
				{
				return;
			}
			var hospital = new Hospital
			{
				Id = Guid.NewGuid(),
				Name = "City Hospital",
				Address = "123 Main St, Anytown",
				Phone = "555-1234",
				IsActive = true,
				CreatedBy = Guid.Empty
			};
			await context.Hospitals.AddAsync(hospital);

			var adminUser = new User
			{
				Id = Guid.NewGuid(),
				HospitalId = hospital.Id,
				Email = "admin@mediconnect.com",
				PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
				IsActive = true,
				CreatedBy = Guid.Empty
			};
			await context.Users.AddAsync(adminUser);

			// Doctor User
			var doctorUser = new User
			{
				Id = Guid.NewGuid(),
				HospitalId = hospital.Id,
				Email = "doctor@mediconnect.com",
				PasswordHash = BCrypt.Net.BCrypt.HashPassword("Doctor@123"),
				IsActive = true,
				CreatedBy = Guid.Empty
			};
			await context.Users.AddAsync(doctorUser);

			// Doctor Profile
			var doctor = new Doctor
			{
				Id = Guid.NewGuid(),
				UserId = doctorUser.Id,
				HospitalId = hospital.Id,
				FirstName = "Ahmed",
				LastName = "Raza",
				LicenseNumber = "LIC-001",
				ConsultationFee = 1500,
				Gender = Gender.Male,
				IsActive = true,
				CreatedBy = adminUser.Id
			};
			await context.Doctors.AddAsync(doctor);

			await context.SaveChangesAsync();

			// Console pe print karo — IDs yaad rakhne ke liye
			Console.WriteLine("=================================");
			Console.WriteLine($"Hospital ID: {hospital.Id}");
			Console.WriteLine($"Doctor ID:   {doctor.Id}");
			Console.WriteLine($"Admin Email: admin@mediconnect.com");
			Console.WriteLine($"Admin Pass:  Admin@123");
			Console.WriteLine("=================================");
		}

		
		
	}
}
