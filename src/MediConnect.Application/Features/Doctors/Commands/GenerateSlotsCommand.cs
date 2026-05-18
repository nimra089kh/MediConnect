using MediatR;
using MediConnect.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediConnect.Application.Features.Doctors.Commands
{
	public record GenerateSlotsCommand(
		Guid DoctorId,
		Guid HospitalId,
		DateOnly FromDate,
		DateOnly ToDate
		) : IRequest<Result<int>>
	{
	}
}
