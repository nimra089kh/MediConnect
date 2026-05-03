using MediatR;
using MediConnect.Application.Common.Models;
using MediConnect.Application.Features.Auth.DTOs;
using MediConnect.Application.Interfaces.Repositories;
using MediConnect.Application.Interfaces.Services;
using MediConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Application.Features.Auth.Commands
{
	public class LoginCommandHandler
	: IRequestHandler<LoginCommand, Result<AuthResponseDto>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IJwtService _jwtService;

		public LoginCommandHandler(IUnitOfWork unitOfWork, IJwtService jwtService)
		{
			_unitOfWork = unitOfWork;
			_jwtService = jwtService;
		}

		public async Task<Result<AuthResponseDto>> Handle(
			LoginCommand request,
			CancellationToken cancellationToken)
		{
			// Step 1 — Find user by email
			var user = await _unitOfWork.Users
				.GetByEmailAsync(request.Email, cancellationToken);

			if (user is null || !user.IsActive)
				return Result<AuthResponseDto>.Failure(
					"Invalid email or password.");

			// Step 2 — Verify password
			var passwordValid = BCrypt.Net.BCrypt
				.Verify(request.Password, user.PasswordHash);

			if (!passwordValid)
				return Result<AuthResponseDto>.Failure(
					"Invalid email or password.");

			// Step 3 — Determine role
			var role = user.Doctor is not null ? "Doctor"
					 : user.Patient is not null ? "Patient"
					 : "Admin";

			// Step 4 — Generate tokens
			var accessToken = _jwtService.GenerateAccessToken(user, role);
			var refreshToken = _jwtService.GenerateRefreshToken();

			// Step 5 — Save refresh token to database
			var refreshTokenEntity = new RefreshToken
			{
				UserId = user.Id,
				Token = refreshToken,
				ExpiresAt = DateTime.UtcNow.AddDays(7),
				CreatedBy = user.Id
			};

			await _unitOfWork.RefreshTokens
			.AddAsync(refreshTokenEntity, cancellationToken);

			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return Result<AuthResponseDto>.Success(new AuthResponseDto
			{
				AccessToken = accessToken,
				RefreshToken = refreshToken,
				Email = user.Email,
				Role = role,
				UserId = user.Id,
				HospitalId = user.HospitalId


	});
		}
	}
}
