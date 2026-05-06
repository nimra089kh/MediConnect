using MediConnect.Application.Interfaces.Repositories;
using MediConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Infrastructure.Persistence.Repositories
{
	public class PatientRepository : GenericRepository<Patient>, IPatientRepository
	{
		public PatientRepository(ApplicationDbContext context)
			: base(context) { }

		public async Task<Patient?> GetByUserIdAsync(
			Guid userId,
			CancellationToken cancellationToken = default)
		{
			return await _dbSet
				.Include(p => p.User)
				.Include(p => p.MedicalHistories)
				.FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);
		}
		public async Task<IEnumerable<Patient>> GetByHospitalIdAsync(
			Guid hospitalId,
			CancellationToken cancellationToken = default)
		{
			return await _dbSet
				.Where(p => p.HospitalId == hospitalId && p.IsActive)
				.ToListAsync(cancellationToken);
		}
	}
}
