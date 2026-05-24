using MediatR;
using MediConnect.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediConnect.Application.Features.Doctors.Commands
{
	public record SetAvailabilityCommand(
		Guid DoctorId,
		Guid HospitalId,
		DayOfWeek DayOfWeek, 
		TimeOnly StartTime, 
		TimeOnly EndTime, 
		int SlotDurationMinutes = 30) : IRequest<Result>;

}
