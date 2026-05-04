using MediConnect.Application.Interfaces.Repositories;
using MediConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Infrastructure.Persistence.Repositories
{
	public class UserRepository : GenericRepository<User>, IUserRepository
	{
		public UserRepository(ApplicationDbContext context) : base(context)
		{ }
		 
		public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
		{
			return await _dbSet
				.Include(u =>u.Doctor)
				.Include(u => u.Patient)
				.FirstOrDefaultAsync(u => u.Email == email.ToLower().Trim(), cancellationToken);
		}

		public async Task<bool> EmailExistAsymc(string email, CancellationToken cancellationToken = default)
		{
			return await _dbSet.AnyAsync(u => u.Email == email.ToLower().Trim(), cancellationToken);
		}

		public async Task AddRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
		{
			await _context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
			
		}

	}
}
