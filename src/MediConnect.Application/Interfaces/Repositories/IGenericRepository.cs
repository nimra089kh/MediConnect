using MediConnect.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Application.Interfaces.Repositories
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		Task<T> GetByIdAsync(
			Guid id,
			CancellationToken cancellationToken = default
			);
		Task<IEnumerable<T>> GetAllAsync(
			CancellationToken cancellationToken = default
			);
		Task AddAsync(
			T entity,
			CancellationToken cancellationToken = default
			);
		void update(T entity);
		void delete(T entity);

	}
}
