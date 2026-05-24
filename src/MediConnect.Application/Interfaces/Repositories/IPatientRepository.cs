using MediConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Application.Interfaces.Repositories
{
	public interface IPatientRepository: IGenericRepository<Patient>
	{
		Task<Patient?> GetByUserIdAsync(
	 Guid userId,
	 CancellationToken cancellationToken = default);

		Task<IEnumerable<Patient>> GetByHospitalIdAsync(
			Guid hospitalId,
			CancellationToken cancellationToken = default);
	}
}
