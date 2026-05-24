using MediatR;
using MediConnect.Application.Common.Models;
using MediConnect.Application.Interfaces.Repositories;
using MediConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediConnect.Application.Features.Doctors.Commands
{
	public class SetAvailabilityCommandHandler : IRequestHandler<SetAvailabilityCommand , Result>
	{
		private readonly IUnitOfWork _unitOfWork;
		public SetAvailabilityCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<Result> Handle(
			SetAvailabilityCommand request, 
			CancellationToken cancellationToken)
		{
			var doctor = await _unitOfWork.Doctors.GetByIdAsync(request.DoctorId , cancellationToken);

			if (doctor == null || doctor.HospitalId != request.HospitalId)
				return Result.Failure("Doctor not found");

			if(request.StartTime >= request.EndTime)
				return Result.Failure("Start time must be before end time.");


			var existing = await _unitOfWork.Availabilities
				.GetByDoctorIdAsync(request.DoctorId, cancellationToken);

			var duplicate = existing.Any(a =>
				a.DayOfWeek == request.DayOfWeek &&
				a.StartTime == request.StartTime &&
				a.EndTime == request.EndTime);

			if (duplicate)
				return Result.Failure(
					"Availability for this day and time already exists.");

			var availability = new DoctorAvailability
			{
				DoctorId = request.DoctorId,
				DayOfWeek = request.DayOfWeek,
				StartTime = request.StartTime,
				EndTime = request.EndTime,
				SlotDurationMinutes = request.SlotDurationMinutes,
				IsAvailable = true,
				CreatedBy = request.DoctorId,
			};
			await _unitOfWork.Availabilities.AddAsync(availability, cancellationToken);
			await _unitOfWork.SaveChangesAsync(cancellationToken);
			 return Result.Success();
		}

	}
}
