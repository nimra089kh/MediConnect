using MediConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediConnect.Application.Interfaces.Repositories
{
	public interface IDoctorAvailabilityRepository : IGenericRepository<DoctorAvailability>
	{
		Task<IEnumerable<DoctorAvailability>> GetByDoctorIdAsync(
			Guid DoctorId,
			CancellationToken cancellationToken
			);
	}
}
