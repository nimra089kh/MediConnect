using MediatR;
using MediConnect.Application.Common.Models;
using MediConnect.Application.Features.Appointments.DTOs;
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
	public class BookAppointmentCommandHandler : IRequestHandler<BookAppointmentCommand, Result<AppointmentResponseDto>>
	{
		private readonly IUnitOfWork _unitOfWork;
		public BookAppointmentCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<Result<AppointmentResponseDto>> Handle(
			BookAppointmentCommand request,
			CancellationToken cancellationToken)
		{
			var slots = await _unitOfWork.Slots.GetByIdAsync(request.SlotId, cancellationToken);
			if (slots is null || slots.HospitalId != request.HospitalId)
				return Result<AppointmentResponseDto>
					.Failure("Slot not found.");

			if (slots.IsBooked) 
				return Result<AppointmentResponseDto>
				.Failure("This slot is already booked.");

			var doctor = await _unitOfWork.Doctors.GetByIdAsync(
				request.DoctorId, 
				cancellationToken);

			if (doctor is null || doctor.HospitalId != request.HospitalId)
				return Result<AppointmentResponseDto>
					.Failure("Doctor not found.");

			var patient = await _unitOfWork.Patients
		   .GetByUserIdAsync(request.PatientId, cancellationToken);

			if (patient is null)
				return Result<AppointmentResponseDto>
					.Failure("Patient profile not found.");

			var appointment = new Appointment
			{
				HospitalId = request.HospitalId,
				PatientId = patient.Id,
				DoctorId = request.DoctorId,
				SlotId = request.SlotId,
				AppointmentDate = request.AppointmentDate,
				StartTime = slots.StartTime,
				EndTime = slots.EndTime,
				Status = AppointmentStatus.Pending,
				Notes = request.Notes,
				CreatedBy = request.PatientId
			};
			// STEP 2 — Slot mark as booked
			slots.IsBooked = true;
			_unitOfWork.Slots.update(slots);

			// STEP 3 — Status history
			var statusHistory = new AppointmentStatusHistory
			{
				AppointmentId = appointment.Id,
				OldStatus = AppointmentStatus.Pending,
				NewStatus = AppointmentStatus.Pending,
				ChangedBy = request.PatientId,
				Reason = "Appointment booked by patient",
				CreatedBy = request.PatientId
			};

			await _unitOfWork.Appointments
			.AddAsync(appointment, cancellationToken);
			await _unitOfWork.StatusHistories
				.AddAsync(statusHistory, cancellationToken);

			// ONE SaveChangesAsync — atomic transaction
			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return Result<AppointmentResponseDto>.Success(
				new AppointmentResponseDto
				{
					Id = appointment.Id,
					DoctorFullName = doctor.FullName,
					PatientFullName = patient.FullName,
					AppointmentDate = appointment.AppointmentDate,
					StartTime = appointment.StartTime,
					EndTime = appointment.EndTime,
					Status = appointment.Status.ToString(),
					ConsultationFees = doctor.ConsultationFee,
					Notes = appointment.Notes
				});

		}



	}
}
