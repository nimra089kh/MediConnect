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
	public class UpdateAppointmentStatusCommandHandler
	: IRequestHandler<UpdateAppointmentStatusCommand, Result>
	{
		private readonly IUnitOfWork _unitOfWork;

		public UpdateAppointmentStatusCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<Result> Handle(
			UpdateAppointmentStatusCommand request,
			CancellationToken cancellationToken)
		{
			
			var appointment = await _unitOfWork.Appointments
				.GetByIdAsync(request.AppointmentId, cancellationToken);

			if (appointment is null ||
				appointment.HospitalId != request.HospitalId)
				return Result.Failure("Appointment not found.");

			
			if (appointment.Status != AppointmentStatus.Pending)
				return Result.Failure(
					"Only pending appointments can be confirmed or rejected.");

			
			var doctor = await _unitOfWork.Doctors
				.GetByUserIdAsync(request.DoctorUserId, cancellationToken);

			if (doctor is null || appointment.DoctorId != doctor.Id)
				return Result.Failure(
					"You can only update your own appointments.");

			
			var oldStatus = appointment.Status;

			
			appointment.Status = request.Action == AppointmentAction.Confirm
				? AppointmentStatus.Confirmed
				: AppointmentStatus.Rejected;

		
			if (request.Action == AppointmentAction.Reject)
			{
				var slot = await _unitOfWork.Slots
					.GetByIdAsync(appointment.SlotId, cancellationToken);

				if (slot is not null)
				{
					slot.IsBooked = false;
					_unitOfWork.Slots.update(slot);
				}
			}

			_unitOfWork.Appointments.update(appointment);

			
			var statusHistory = new AppointmentStatusHistory
			{
				AppointmentId = appointment.Id,
				OldStatus = oldStatus,
				NewStatus = appointment.Status,
				ChangedBy = request.DoctorUserId,
				Reason = request.Reason ??
							   (request.Action == AppointmentAction.Confirm
								   ? "Confirmed by doctor"
								   : "Rejected by doctor"),
				CreatedBy = request.DoctorUserId
			};

			await _unitOfWork.StatusHistories
				.AddAsync(statusHistory, cancellationToken);

			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return Result.Success();
		}
	}
	}
