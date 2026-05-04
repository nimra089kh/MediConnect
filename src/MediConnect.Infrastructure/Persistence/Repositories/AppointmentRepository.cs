using MediConnect.Application.Interfaces.Repositories;
using MediConnect.Domain.Entities;
using MediConnect.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Infrastructure.Persistence.Repositories
{
	public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
	{
		public AppointmentRepository(ApplicationDbContext context) : base(context)
		{
		}

		public async Task<IEnumerable<Appointment>> GetByDoctorIdAsync(
			Guid doctorId,
			DateOnly date,
			CancellationToken cancellationToken = default)
		{
			return await _dbSet
				.Include(a => a.Patient)
				.Include(a => a.Slot)
				.Where(a => a.DoctorId == doctorId && a.AppointmentDate == date && a.Status != AppointmentStatus.Canceled)
				.OrderBy(a => a.StartTime)
				.ToListAsync(cancellationToken);
		}
		public async Task<IEnumerable<Appointment>> GetByPatientIdAsync(
			Guid patientId,
			CancellationToken cancellationToken = default)
		{
			return await _dbSet
				.Include(a => a.Doctor)
				.Include(a => a.Hospital)
				.Where(a => a.PatientId == patientId)
				.OrderByDescending(a => a.AppointmentDate)
				.ToListAsync(cancellationToken);
		}
		public async Task<IEnumerable<Appointment>> GetByHospitalIdAsync(
			Guid hospitalId,
			AppointmentStatus? status,
			CancellationToken cancellationToken = default)
		{
			var query = _dbSet
				.Include(a => a.Doctor)
				.Include(a => a.Patient)
				.Where(a => a.HospitalId == hospitalId);
			if (status.HasValue)
			{
				query = query.Where(a => a.Status == status.Value);
			}
			return await query
				.OrderByDescending(a => a.AppointmentDate)
				.ToListAsync(cancellationToken);
		}

		public async Task<bool> HasConflictingAppointmentAsync(Guid doctorId,
			DateOnly date,
			TimeOnly starttime,
			TimeOnly endTime,
			CancellationToken cancellationToken = default)
		{
			return await _dbSet.AnyAsync(
				a => a.DoctorId == doctorId
				&& a.AppointmentDate == date
				&& a.Status != AppointmentStatus.Canceled
				&& a.StartTime < endTime
				&& a.EndTime > starttime
				, cancellationToken);
		}
	}
}
