using MediatR;
using MediConnect.Application.Common.Models;
using MediConnect.Application.Features.Auth.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Application.Features.Auth.Commands
{
	public record LoginCommand(
		string Email,
		string Password
		) : IRequest<Result<AuthResponseDto>>;
}
