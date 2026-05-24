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
	public class GenerateSlotsCommandHandler : IRequestHandler<GenerateSlotsCommand, Result<int>>
	{
		private readonly IUnitOfWork _unitOfWork;
		public GenerateSlotsCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}


		public async Task<Result<int>> Handle(
			GenerateSlotsCommand request,
			CancellationToken cancellationToken)
		{
			// Doctor check
			var doctor = await _unitOfWork.Doctors
				.GetByIdAsync(request.DoctorId, cancellationToken);

			if (doctor == null || doctor.HospitalId != request.HospitalId)
				return Result<int>.Failure("Doctor not found.");
			


			var availabilities = await _unitOfWork.Availabilities
				.GetByDoctorIdAsync(request.DoctorId, cancellationToken);

			if (!availabilities.Any())
				return Result<int>.Failure(
			   "No availability set for doctor.");

			var slotsCreated = 0;
			var currentDate = request.FromDate;

			while (currentDate <= request.ToDate)
			{
				var dayAvailabilities = availabilities
					.Where(a => a.DayOfWeek == currentDate.DayOfWeek && a.IsAvailable);

				foreach (var availability in dayAvailabilities)
				{
					var slotStart = availability.StartTime;
					while (slotStart.AddMinutes(availability.SlotDurationMinutes) <= availability.EndTime)
					{
						var slotEnd = slotStart.AddMinutes(availability.SlotDurationMinutes);
						var exist = await _unitOfWork.Slots
							.SlotsExistsAsync
							(request.DoctorId,
							currentDate,
							slotStart,
							cancellationToken);

						if (!exist)
						{
							var slot = new AppointmentSlot
							{
								DoctorId = request.DoctorId,
								HospitalId = request.HospitalId,
								SlotDate = currentDate,
								StartTime = slotStart,
								EndTime = slotEnd,
								IsBooked = false,
								CreatedBy = request.DoctorId,
							};
							await _unitOfWork.Slots.AddAsync(slot, cancellationToken);
							slotsCreated++;
						}
						slotStart = slotEnd;

					}
				}
				currentDate = currentDate.AddDays(1);


			}
			await _unitOfWork.SaveChangesAsync(cancellationToken);
			return Result<int>.Success(slotsCreated);
		}
		
	}
}

