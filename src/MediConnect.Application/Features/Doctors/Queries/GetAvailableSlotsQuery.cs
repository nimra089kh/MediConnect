using MediatR;
using MediConnect.Application.Common.Models;
using MediConnect.Application.Features.Doctors.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediConnect.Application.Features.Doctors.Queries
{
	public record GetAvailableSlotsQuery(
	Guid DoctorId,
	DateOnly Date,
	Guid HospitalId
) : IRequest<Result<IEnumerable<AvailableSlotDto>>>;
}
