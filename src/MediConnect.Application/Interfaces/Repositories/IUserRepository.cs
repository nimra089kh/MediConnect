using MediConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Application.Interfaces.Repositories
{
	public interface IUserRepository : IGenericRepository<User>
	{
		Task<User?> GetByEmailAsync(
			string email,
			CancellationToken cancellationToken = default
			);
		Task<bool> EmailExistAsymc(
			string email,
			CancellationToken cancellationToken = default
			);
		Task AddRefreshTokenAsync(RefreshToken refreshTokenEntity, CancellationToken cancellationToken);
	}
}
