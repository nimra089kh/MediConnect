using MediatR;
using MediConnect.Application.Common.Models;
using MediConnect.Application.Features.Appointments.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediConnect.Application.Features.Appointments.Commands
{
	public record BookAppointmentCommand(
		Guid PatientId,
		Guid DoctorId,
		Guid HospitalId,
		Guid SlotId,
		DateOnly AppointmentDate,
		string? Notes) : IRequest<Result<AppointmentResponseDto>>
	{
	}
}
