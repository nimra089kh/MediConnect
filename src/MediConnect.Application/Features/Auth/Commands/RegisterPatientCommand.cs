using MediatR;
using MediConnect.Application.Common.Models;
using MediConnect.Application.Features.Auth.DTOs;
using MediConnect.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Application.Features.Auth.Commands
{
	public record RegisterPatientCommand
	(
		string FirstName,
		string LastName,
		string Email,
		string Password,
		Guid HospitalId,
		DateOnly? DateOfBirth,
		Gender? Gender,
		string? Phone

	): IRequest<Result<AuthResponseDto>>;
}
