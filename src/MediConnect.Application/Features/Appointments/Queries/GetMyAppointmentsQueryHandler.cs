using MediatR;
using MediConnect.Application.Common.Models;
using MediConnect.Application.Features.Appointments.DTOs;
using MediConnect.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Application.Features.Appointments.Queries { 
	public class GetMyAppointmentsQueryHandler
	: IRequestHandler<GetMyAppointmentsQuery, Result<IEnumerable<AppointmentResponseDto>>>
	{
		private readonly IUnitOfWork _UnitOfWork;

		public GetMyAppointmentsQueryHandler(IUnitOfWork IUnitOfWork)
		{
			_UnitOfWork = IUnitOfWork;
		}

		public async Task<Result<IEnumerable<AppointmentResponseDto>>> Handle(GetMyAppointmentsQuery request, CancellationToken cancellationToken)
		{
			var patient = await _UnitOfWork.Patients
				.GetByUserIdAsync(request.PatientId, cancellationToken);

			if (patient is null)
				return Result<IEnumerable<AppointmentResponseDto>>
					.Success(Enumerable.Empty<AppointmentResponseDto>());



			var appointments = await _UnitOfWork.Appointments
	.GetByPatientIdAsync(patient.Id, cancellationToken);
			var filtered = appointments.Where(a => a.HospitalId == request.HospitalId);
				var response = filtered.Select(a => new AppointmentResponseDto
				{
					Id = a.Id,
					DoctorFullName = a.Doctor?.FullName ?? "N/A",
					PatientFullName = a.Patient?.FullName ?? "N/A",
					AppointmentDate = a.AppointmentDate,
					StartTime = a.StartTime,
					EndTime = a.EndTime,
					Status = a.Status.ToString(),
					ConsultationFees = a.Doctor?.ConsultationFee ?? 0,
					Notes = a.Notes
				});
				return Result<IEnumerable<AppointmentResponseDto>>.Success(response);

		}
	}
}
