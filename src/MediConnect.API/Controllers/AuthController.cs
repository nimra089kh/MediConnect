using MediatR;
using MediConnect.Application.Features.Auth.Commands;
using MediConnect.Application.Features.Auth.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MediConnect.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{

		private readonly IMediator _mediator;
		public AuthController(IMediator mediator)
		{
			_mediator = mediator;

		}
		[HttpPost("login")]
		public async Task<IActionResult> Login(
		[FromBody] LoginRequestDtos request)
		{
			var command = new LoginCommand(request.Email, request.Password);
			var result = await _mediator.Send(command);

			if (!result.IsSuccess)
			{
				return BadRequest(new { error = result.Error });
			}
			return Ok(result.Data);
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register(
			[FromBody] RegisterPatientDto request)
		{
			var command = new RegisterPatientCommand(
				request.FirstName,
				request.LastName,
				request.Email,
				request.Password,
				request.HospitalId,
				request.DateOfBirth,
				request.Gender,
				request.Phone
				);
			var result = await _mediator.Send(command);
			if (!result.IsSuccess)
			{
				return BadRequest(new { error = result.Error });
			}
			return Ok(result.Data);
		}

		}
}
