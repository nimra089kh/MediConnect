using MediConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Infrastructure.Persistence.Configurations
{
	public class PatientConfiguration : IEntityTypeConfiguration<Patient>
	{
		public void Configure(EntityTypeBuilder<Patient> builder)
		{
		builder.HasKey(p => p.Id);

			builder.Property(p => p.FirstName)
			.IsRequired()
			.HasMaxLength(50);

			builder.Property(p => p.LastName)
				.IsRequired()
				.HasMaxLength(50);

			builder.Ignore(p => p.FullName);
			builder.Ignore(p => p.Age);

			builder.HasIndex(p => p.HospitalId);

			builder.HasMany(p => p.MedicalHistories)
				.WithOne(m => m.Patient)
				.HasForeignKey(m => m.PatientId)
				.OnDelete(DeleteBehavior.Restrict);


		}

	}
}
