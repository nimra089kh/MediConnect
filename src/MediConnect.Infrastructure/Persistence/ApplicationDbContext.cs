using MediConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Infrastructure.Persistence
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{}

		public DbSet<Hospital> Hospitals => Set<Hospital>();
		public DbSet<Department> Departments => Set<Department>();
		public DbSet<User> Users => Set<User>();
		public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
		public DbSet<Doctor> Doctors => Set<Doctor>();
		public DbSet<Patient> Patients => Set<Patient>();
		public DbSet<DoctorAvailability> DoctorAvailabilities => Set<DoctorAvailability>();
		public DbSet<Appointment> Appointments => Set<Appointment>();
		public DbSet<AppointmentSlot> AppointmentSlots => Set<AppointmentSlot>();
		public DbSet<AppointmentStatusHistory> AppointmentStatusHistories => Set<AppointmentStatusHistory>();
		public DbSet<MedicalHistory> MedicalHistories => Set<MedicalHistory>();
		public DbSet<MedicalDocument> MedicalDocuments => Set<MedicalDocument>();
		public DbSet<Payment> Payments => Set<Payment>();
		public DbSet<Refund> Refunds => Set<Refund>();
		public DbSet<Notification> Notifications => Set<Notification>();
		public DbSet<Feedback> Feedbacks => Set<Feedback>();
		public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
		public DbSet<Specialization> Specializations => Set<Specialization>();


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(
				typeof(ApplicationDbContext).Assembly);

			base.OnModelCreating(modelBuilder);
		}

		public override async Task<int> SaveChangesAsync(
			CancellationToken cancellationToken = default
			)
		{
			foreach (var entry in ChangeTracker.Entries<Domain.Common.BaseEntity>())
			{
				if (entry.State == EntityState.Modified)
				{
					entry.Entity.UpdatedAt = DateTime.UtcNow;
				}
			}
			return await base.SaveChangesAsync(cancellationToken);

		}



	}
}
