using MediConnect.Domain.Entities;
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

			await context.SaveChangesAsync();
		}
	}
}
