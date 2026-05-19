using MediatR;
using MediConnect.Application.Common.Models;
using MediConnect.Application.Interfaces.Repositories;
using MediConnect.Domain.Entities;
using MediConnect.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediConnect.Application.Features.Appointments.Commands
{
	public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand, Result>
	{
		private readonly IUnitOfWork _UnitOfWork;

		public CancelAppointmentCommandHandler(IUnitOfWork IUnitOfWork)
		{
			_UnitOfWork = IUnitOfWork;
		}

		public async Task<Result> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
		{
			var appointment = await _UnitOfWork.Appointments.GetByIdAsync(request.AppointmentId, cancellationToken);
			if (appointment is null ||
			appointment.HospitalId != request.HospitalId)
				return Result.Failure("Appointment not found.");

			if(appointment.Status ==AppointmentStatus.Canceled )
				return Result.Failure("Appointment is already canceled.");


			if (appointment.Status == AppointmentStatus.Completed)
				return Result.Failure("Completed appointment cannot be cancelled.");

			if(request.Role == "Patient")
			{
				if (appointment.Status == AppointmentStatus.Confirmed)
					return Result.Failure("Confirmed appointments can only be cancelled by Doctor.");

				var appointmentDateTime = appointment.AppointmentDate
	.ToDateTime(appointment.StartTime);
				var hoursUntilAppointment = (appointmentDateTime - DateTime.UtcNow).TotalHours;

				if (hoursUntilAppointment < 2)
					return Result.Failure(
						"Cannot cancel appointment less than 2 hours before scheduled time.");

			}

			if (request.Role == "Doctor")
			{
				var doctor = await _UnitOfWork.Doctors
				.GetByUserIdAsync(request.UserId, cancellationToken);

				if (doctor is null || appointment.DoctorId != doctor.Id)
					return Result.Failure(
						"You can only cancel your own appointments.");
			}

			appointment.Status = AppointmentStatus.Canceled;
			appointment.CancelledBy = request.UserId;
			appointment.CancellationReason = request.Reason;
			appointment.CancelledAt = DateTime.UtcNow;

			_UnitOfWork.Appointments.update(appointment);

			var slot = await _UnitOfWork.Slots
		   .GetByIdAsync(appointment.SlotId, cancellationToken);

			if (slot is not null)
			{
				slot.IsBooked = false;
				_UnitOfWork.Slots.update(slot);
			}
			var statusHistory = new AppointmentStatusHistory
			{
				AppointmentId = appointment.Id,
				OldStatus = appointment.Status,
				NewStatus = AppointmentStatus.Canceled,
				ChangedBy = request.UserId,
				Reason = request.Reason,
				CreatedBy = request.UserId
			};

			await _UnitOfWork.StatusHistories
				.AddAsync(statusHistory, cancellationToken);

			// ONE transaction — sab ek saath
			await _UnitOfWork.SaveChangesAsync(cancellationToken);

			return Result.Success();
			
		}
	}
}
