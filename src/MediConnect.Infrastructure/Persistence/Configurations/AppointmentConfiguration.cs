using MediConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Infrastructure.Persistence.Configurations
{
	public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
	{
		public void Configure(EntityTypeBuilder<Appointment> builder)
		{ 
			builder.HasKey(a => a.Id);

			builder.Property(a => a.Status)
				.HasConversion<int>();

			builder.Property(a => a.Notes)
				.HasMaxLength(100);
			builder.HasIndex(a => a.HosptalId);
			builder.HasIndex(a => a.PatientId);
			builder.HasIndex(a => a.DoctorId);
			builder.HasIndex(a => a.AppointmentDate);
			builder.HasIndex(a => a.Status);

			builder.HasOne(a => a.Doctor)
				.WithMany(d => d.Appointments)
				.HasForeignKey(a => a.DoctorId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(a => a.Patient)
				.WithMany(p => p.Appointments)
				.HasForeignKey(a => a.PatientId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne( a => a.RescheduledFrom)
				.WithMany()
				.HasForeignKey(a => a.RescheduledFromId)
				.OnDelete(DeleteBehavior.Restrict); 

		}
	}
}
