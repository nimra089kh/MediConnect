using MediatR;
using MediConnect.Application.Features.Doctors.Commands;
using MediConnect.Application.Features.Doctors.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediConnect.Application.Features.Doctors.Queries;

namespace MediConnect.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class DoctorsController : Controller
	{

		private readonly IMediator _mediator;
		public DoctorsController(IMediator mediator)
		{
			_mediator = mediator;

		}

		private Guid GetHospitalId() =>
		   Guid.Parse(User.FindFirst("HospitalId")!.Value);


		[HttpPost("availability")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> SetAvailibility(
			[FromBody] SetAvailabilityDto request
			)
		{
			var command = new SetAvailabilityCommand(
				DoctorId: request.DoctorId,
				HospitalId: GetHospitalId(),
				DayOfWeek: request.DayOfWeek,
				StartTime: request.StartTime,
				EndTime: request.EndTime,
				SlotDurationMinutes: request.SlotDurationMinutes);

			var result = await _mediator.Send(command);

			if (!result.IsSuccess)
				return BadRequest(new { error = result.Error });

			return Ok(new { message = "Availability set successfully." });
		}

		[HttpPost("generate-slots")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GenerateSlots(
			[FromBody] GenerateSlotsDto request
			)
		{
			var command = new GenerateSlotsCommand(
				 DoctorId: request.DoctorId,
			HospitalId: GetHospitalId(),
			FromDate: request.FromDate,
			ToDate: request.ToDate);
			var result = await _mediator.Send(command);

			if (!result.IsSuccess)
				return BadRequest(new { error = result.Error });

			return Ok(new { message = $"{result.Data} slots generated." });
		}

		// Patient: Available slots dekho
		[HttpGet("{doctorId}/slots")]
		[Authorize(Roles = "Patient")]
		public async Task<IActionResult> GetAvailableSlots(
			Guid doctorId,
			[FromQuery] DateOnly date)
		{
			var slots = await _mediator.Send(
				new GetAvailableSlotsQuery(doctorId, date, GetHospitalId()));

			return Ok(slots.Data);
		}

	}
		

}
