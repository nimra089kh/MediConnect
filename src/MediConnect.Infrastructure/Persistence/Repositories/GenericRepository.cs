using MediConnect.Application.Interfaces.Repositories;
using MediConnect.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Infrastructure.Persistence.Repositories
{
	public class GenericRepository<T> :IGenericRepository<T> where T : BaseEntity
	{
		protected readonly ApplicationDbContext _context;
		protected readonly DbSet<T> _dbSet;

		public GenericRepository(ApplicationDbContext context)
		{
			_context = context;
			_dbSet = _context.Set<T>();
		}

		public async Task<T> GetByIdAsync(Guid id , CancellationToken cancellationToken = default)
		{
			return await _dbSet.FindAsync(new object[] { id } , cancellationToken);
		}
		public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			return await _dbSet.ToListAsync(cancellationToken);
		}
		public async Task AddAsync(T entity , CancellationToken cancellationToken = default)
		{
			await _dbSet.AddAsync(entity , cancellationToken);
		}
		public void Update(T entity)
		{
			_dbSet.Update(entity);
		}
		public void Delete(T entity)
		{
			if (entity is BaseEntity baseEntity)
			{
				_context.Entry(entity).State = EntityState.Modified;
			}
			
		}


	}
}
