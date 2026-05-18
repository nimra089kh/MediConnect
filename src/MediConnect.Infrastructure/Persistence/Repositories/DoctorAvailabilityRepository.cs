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
	public class DoctorAvailabilityRepository : GenericRepository<DoctorAvailability>, IDoctorAvailabilityRepository
	{
		public DoctorAvailabilityRepository(ApplicationDbContext context) : base(context)
		{
		}
		public async Task<IEnumerable<DoctorAvailability>> GetByDoctorIdAsync(
			Guid DoctorId,
			CancellationToken cancellationToken)
		{
			return await _dbSet
				.Where(a => a.DoctorId == DoctorId && a.IsAvailable)
				.ToListAsync(cancellationToken);
		}
	
	}
}
