using MediConnect.Domain.Entities;
using MediConnect.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Application.Interfaces.Repositories
{
	public interface IAppointmentRepository : IGenericRepository<Appointment>
	{
		Task<IEnumerable<Appointment>> GetByDoctorIdAsync(
			Guid doctorId,
			DateOnly date,
			CancellationToken cancellationToken = default
			);

		Task<IEnumerable<Appointment>> GetByPatientIdAsync(
			Guid patientId,
			CancellationToken cancellationToken = default
			);
		Task<IEnumerable<Appointment>> GetByHospitalIdAsync(
			Guid hospitalId,
			AppointmentStatus? status,
			CancellationToken cancellationToken = default
			);
		Task<bool> HasConflictingAppointmentAsync(
			Guid doctorId,
			DateOnly date,
			TimeOnly StartTime,
			TimeOnly EndTime,
			CancellationToken cancellationToken = default
			);

	}
}
