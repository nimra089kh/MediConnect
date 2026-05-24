using MediConnect.Application.Interfaces.Repositories;
using MediConnect.Domain.Entities;
using MediConnect.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Infrastructure.Persistence
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _context;

		private IAppointmentRepository? _appointments;
		private IUserRepository? _users;
		private IPatientRepository? _patients;
		private IDoctorRepository? _doctors;
		private IGenericRepository<RefreshToken>? _refreshTokens;
		private IGenericRepository<Hospital>? _hospitals;
		private IDoctorAvailabilityRepository? _availabilities;
		private ISlotRepository? _slots;
		private IGenericRepository<AppointmentStatusHistory>? _statusHistories;

		

		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
		}

		public IAppointmentRepository Appointments => _appointments ??= new AppointmentRepository(_context);
		public IUserRepository Users => _users ??= new UserRepository(_context);
		public IPatientRepository Patients => _patients ??= new PatientRepository(_context);
		public IDoctorRepository Doctors => _doctors ??= new DoctorRepository(_context);
		public IGenericRepository<RefreshToken> RefreshTokens => _refreshTokens ??= new GenericRepository<RefreshToken>(_context);
		public IGenericRepository<Hospital> Hospitals => _hospitals ??= new GenericRepository<Hospital>(_context);
		public IDoctorAvailabilityRepository Availabilities =>
	   _availabilities ??= new DoctorAvailabilityRepository(_context);
		public IGenericRepository<AppointmentStatusHistory> StatusHistories =>
			_statusHistories ??=
				new GenericRepository<AppointmentStatusHistory>(_context);

		public ISlotRepository Slots =>
			_slots ??= new SlotRepository(_context);
		public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return await _context.SaveChangesAsync(cancellationToken);
		}

		public void Dispose()
		{
			_context.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
