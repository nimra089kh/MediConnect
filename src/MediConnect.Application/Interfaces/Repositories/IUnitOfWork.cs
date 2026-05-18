using MediConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Application.Interfaces.Repositories
{
	public interface IUnitOfWork : IDisposable
	{
		IAppointmentRepository Appointments { get; }
		IDoctorRepository Doctors { get; }
		IPatientRepository Patients { get; }
		IUserRepository Users { get; }
		IGenericRepository<RefreshToken> RefreshTokens { get; }
		IGenericRepository<Hospital> Hospitals { get; }
		IDoctorAvailabilityRepository Availabilities { get; }
		ISlotRepository Slots { get; }
		IGenericRepository<AppointmentStatusHistory> StatusHistories { get; }
		Task<int> SaveChangesAsync(
			CancellationToken cancellationToken = default
			);
	}
}
