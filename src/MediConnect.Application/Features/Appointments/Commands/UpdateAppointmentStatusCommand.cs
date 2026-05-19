using MediatR;
using MediConnect.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediConnect.Application.Features.Appointments.Commands
{
	public record UpdateAppointmentStatusCommand(
	Guid AppointmentId,
	Guid DoctorUserId,
	Guid HospitalId,
	AppointmentAction Action,
	string? Reason
) : IRequest<Result>;

	public enum AppointmentAction
	{
		Confirm = 1,
		Reject = 2
	}
	
}
