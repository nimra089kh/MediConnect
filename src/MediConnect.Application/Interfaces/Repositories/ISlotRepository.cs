using MediConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediConnect.Application.Interfaces.Repositories
{
	public interface ISlotRepository : IGenericRepository<AppointmentSlot>
	{
		Task<bool> SlotsExistsAsync(
			Guid DoctorId,
			DateOnly Date,
			TimeOnly StartTime,
			CancellationToken cancellationToken = default
			);

		Task<IEnumerable<AppointmentSlot>> GetAvailableSlotsAsync(
			Guid DoctorId,
			DateOnly Date,
			CancellationToken cancellationToken = default
			);
	}
}
