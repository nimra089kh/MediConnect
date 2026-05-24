using MediatR;
using MediConnect.Application.Common.Models;
using MediConnect.Application.Features.Appointments.DTOs;
using System;
using System.Collections.Generic;
using System.Text;


namespace MediConnect.Application.Features.Appointments.Queries
{
	public record GetMyAppointmentsQuery(Guid PatientId , Guid HospitalId ) : IRequest<Result<IEnumerable<AppointmentResponseDto>>>
	{
	}
}
