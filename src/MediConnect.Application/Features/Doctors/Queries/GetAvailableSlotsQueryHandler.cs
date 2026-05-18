using MediatR;
using MediConnect.Application.Common.Models;
using MediConnect.Application.Features.Doctors.DTOs;
using MediConnect.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediConnect.Application.Features.Doctors.Queries
{
	public class GetAvailableSlotsQueryHandler
	: IRequestHandler<GetAvailableSlotsQuery, Result<IEnumerable<AvailableSlotDto>>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public GetAvailableSlotsQueryHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<Result<IEnumerable<AvailableSlotDto>>> Handle(
			GetAvailableSlotsQuery request,
			CancellationToken cancellationToken)
		{
			var slots = await _unitOfWork.Slots
				.GetAvailableSlotsAsync(
					request.DoctorId,
					request.Date,
					cancellationToken);

			var filtered = slots
				.Where(s => s.HospitalId == request.HospitalId);

			var response = filtered.Select(s => new AvailableSlotDto
			{
				SlotId = s.Id,
				Date = s.SlotDate,
				StartTime = s.StartTime,
				EndTime = s.EndTime
			});

			return Result<IEnumerable<AvailableSlotDto>>.Success(response);
		}
	}
	}
