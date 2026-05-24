using MediatR;
using MediConnect.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediConnect.Application.Features.Appointments.Commands
{
	public record CancelAppointmentCommand(
		Guid AppointmentId,
		Guid UserId,
		Guid HospitalId,
		string Role,
		string Reason
		) : IRequest<Result>
	{
	}
}
