using MediConnect.Application.Interfaces.Repositories;
using MediConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediConnect.Infrastructure.Persistence.Repositories
{
	public class SlotRepository : GenericRepository<AppointmentSlot>, ISlotRepository
	{
		public SlotRepository(ApplicationDbContext context)
	   : base(context) { }
		public async Task<bool> SlotsExistsAsync(
			Guid DoctorId,
			DateOnly Date,
			TimeOnly StartTime,
			CancellationToken cancellationToken = default)
		{
			return await _dbSet.AnyAsync(s =>
					s.DoctorId == DoctorId &&
					s.SlotDate == Date &&
					s.StartTime == StartTime,
					cancellationToken);
		}

		public async Task<IEnumerable<AppointmentSlot>> GetAvailableSlotsAsync(
			Guid DoctorId,
			DateOnly Date,
			CancellationToken cancellationToken = default)
		{
			return await _dbSet
				.Where(s => s.DoctorId == DoctorId
				&& s.SlotDate == Date
				&& !s.IsBooked)
				.OrderBy(s => s.StartTime)
				.ToListAsync(cancellationToken);

		}
	}
}
