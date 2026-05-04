using MediConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Infrastructure.Persistence.Configurations
{
	public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
	{
		public void Configure(EntityTypeBuilder<Doctor> builder)
		{ builder.HasKey(d => d.Id);

			builder.Property(d => d.FirstName)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(d => d.LastName)
				.IsRequired()
				.HasMaxLength(50);

			builder.Property(d => d.LicenseNumber)
				.IsRequired()
				.HasMaxLength(50);
			builder.HasIndex(d => d.LicenseNumber)
				.IsUnique();

			builder.Property(d => d.ConsultationFee)
				.HasColumnType("decimal(10,2)");

			builder.Ignore(d => d.FullName);

			builder.HasIndex(d => d.HospitalId);

			builder.HasMany(d => d.Departments)
				.WithMany(dep => dep.Doctors)
				.UsingEntity(j => j.ToTable("DoctorDepartments"));

			builder.HasMany(d => d.Specializations)
				.WithMany(s => s.Doctors)
				.UsingEntity(j => j.ToTable("DoctorSpecializations"));


		}

	}
}
