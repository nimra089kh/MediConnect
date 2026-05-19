using MediatR;
using MediConnect.Application.Features.Appointments.Commands;
using MediConnect.Application.Features.Appointments.DTOs;
using MediConnect.Application.Features.Appointments.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MediConnect.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class AppointmentsController : ControllerBase
	{
	
		private readonly IMediator _mediator;
		public AppointmentsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		private Guid GetUserId() =>
		 Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
		

		private Guid GetHospitalId() =>
			 Guid.Parse(User.FindFirst("HospitalId")!.Value);

		
		private string GetRole() =>
			User.FindFirst(ClaimTypes.Role)!.Value;

		[HttpGet("my")]
		[Authorize(Roles = "Patient")]
		public async Task<IActionResult> GetMyAppointments()
		{
			var query = new GetMyAppointmentsQuery(
				PatientId: GetUserId(),
				HospitalId: GetHospitalId());

			var result = await _mediator.Send(query);

			if (!result.IsSuccess)
				return BadRequest(new { error = result.Error });

			return Ok(result.Data);
		}

		[HttpPost("book")]
		[Authorize(Roles = "Patient")]
		public async Task<IActionResult> BookAppointment(
	   [FromBody] BookAppointmentDto request)
		{
			var command = new BookAppointmentCommand(
				PatientId: GetUserId(),
				HospitalId: GetHospitalId(),
				DoctorId: request.DoctorId,
				SlotId: request.SlotId,
				AppointmentDate: request.AppointmentDate,
				Notes: request.Notes);

			var result = await _mediator.Send(command);

			if (!result.IsSuccess)
				return BadRequest(new { error = result.Error });

			return Ok(result.Data);
		}

		// Cancel appointment
		[HttpPut("{appointmentId}/cancel")]
		[Authorize(Roles = "Patient,Doctor,Admin")]
		public async Task<IActionResult> CancelAppointment(
			Guid appointmentId,
			[FromBody] CancelAppointmentDto request)
		{
			var command = new CancelAppointmentCommand(
				AppointmentId: appointmentId,
				UserId: GetUserId(),
				HospitalId: GetHospitalId(),
				Role: GetRole(),
				Reason: request.Reason);

			var result = await _mediator.Send(command);

			if (!result.IsSuccess)
				return BadRequest(new { error = result.Error });

			return Ok(new { message = "Appointment cancelled successfully." });
		}

		// Doctor confirm/reject
		[HttpPut("{appointmentId}/status")]
		[Authorize(Roles = "Doctor")]
		public async Task<IActionResult> UpdateAppointmentStatus(
			Guid appointmentId,
			[FromBody] UpdateAppointmentStatusDto request)
		{
			var command = new UpdateAppointmentStatusCommand(
				AppointmentId: appointmentId,
				DoctorUserId: GetUserId(),
				HospitalId: GetHospitalId(),
				Action: request.Action,
				Reason: request.Reason);

			var result = await _mediator.Send(command);

			if (!result.IsSuccess)
				return BadRequest(new { error = result.Error });

			return Ok(new { message = $"Appointment {request.Action.ToString().ToLower()}ed successfully." });
		}

	}
}
