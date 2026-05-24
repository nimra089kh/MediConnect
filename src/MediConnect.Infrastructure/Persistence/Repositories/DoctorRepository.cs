using MediConnect.Application.Interfaces.Repositories;
using MediConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Infrastructure.Persistence.Repositories
{
	public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
	{
		public DoctorRepository(ApplicationDbContext context)
		: base(context) { }

		public async Task<Doctor?> GetByUserIdAsync(
			Guid userId,
			CancellationToken cancellationToken = default)
		{
			return await _dbSet
			.Include(d => d.User)
			.Include(d => d.Departments)
			.Include(d => d.Specializations)
			.FirstOrDefaultAsync(d => d.UserId == userId, cancellationToken);
		}

		public async Task<IEnumerable<Doctor>> GetByHospitalIdAsync(
			Guid hospitalId,
			CancellationToken cancellationToken = default)
		{
			return await _dbSet
			.Include(d => d.Specializations)
			.Where(d => d.HospitalId == hospitalId && d.IsActive)
			.ToListAsync(cancellationToken);
		}
		public async Task<IEnumerable<AppointmentSlot>> GetAvailableSlotsAsync(
			Guid DoctorId,
			DateOnly date,
			CancellationToken cancellationToken = default)
		{
			return await _context.AppointmentSlots
			.Where(s => s.DoctorId == DoctorId
					 && s.SlotDate == date
					 && !s.IsBooked)
			.OrderBy(s => s.StartTime)
			.ToListAsync(cancellationToken);
		}
	}
}
