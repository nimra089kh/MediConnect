using MediConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Application.Interfaces.Repositories
{
	public interface IDoctorRepository : IGenericRepository<Doctor>
	{
		Task<Doctor?> GetByUserIdAsync(
			Guid userId,
			CancellationToken cancellationToken = default
			);
		Task<IEnumerable<Doctor>> GetByHospitalIdAsync(
			Guid hospitalId,
			CancellationToken cancellationToken = default
			);
		Task<IEnumerable<AppointmentSlot>> GetAvailableSlotsAsync(
			Guid doctorId,
			DateOnly date,
			CancellationToken cancellationToken = default
			);
	}
}
