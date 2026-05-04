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
	public class RegisterPatientCommandHandler : IRequestHandler<RegisterPatientCommand, Result<AuthResponseDto>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IJwtService jwtService;

		public RegisterPatientCommandHandler(IUnitOfWork unitOfWork,
		IJwtService jwtService)
		{
			_unitOfWork = unitOfWork;
			jwtService = jwtService;
		}
		public async Task<Result<AuthResponseDto>> Handle(RegisterPatientCommand request, CancellationToken cancellationToken)
		{
			var EmailExists = await _unitOfWork.Users.EmailExistAsymc(request.Email, cancellationToken);
			if (EmailExists)
			{
				return Result<AuthResponseDto>.Failure("Email already exists");
			}
			var Hospital = await _unitOfWork.Hospitals.GetByIdAsync(request.HospitalId, cancellationToken);
			if (Hospital is null || !Hospital.IsActive)
			{
				return Result<AuthResponseDto>.Failure("Hospital not found");
			}

			var user = new User
			{
				HospitalId = request.HospitalId,
				Email = request.Email,
				PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
				IsActive = true,
				CreatedBy = Guid.Empty,
			};
			await _unitOfWork.Users.AddAsync(user, cancellationToken);

			var patient = new Patient
			{
				UserId = user.Id,
				HospitalId = request.HospitalId,
				FirstName = request.FirstName.Trim(),
				LastName = request.LastName.Trim(),
				DateOfBirth = request.DateOfBirth ?? DateOnly.MinValue,
				Gender = request.Gender ?? Domain.Enums.Gender.Other,
				Phone = request.Phone,
				IsActive = true,
				CreatedBy = user.Id,
			};
			await _unitOfWork.Patients.AddAsync(patient, cancellationToken);

			var accessToken = jwtService.GenerateAccessToken(user, "Patient");
			var refreshToken = jwtService.GenerateRefreshToken();

			var refreshTokenEntity = new RefreshToken
			{
				UserId = user.Id,
				Token = refreshToken,
				ExpiresAt = DateTime.UtcNow.AddDays(7),
				CreatedBy = user.Id,
			};
			await _unitOfWork.RefreshTokens.AddAsync(refreshTokenEntity, cancellationToken);

			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return Result<AuthResponseDto>.Success(new AuthResponseDto
			{
				AccessToken = accessToken,
				RefreshToken = refreshToken,
				Email = user.Email,
				UserId = user.Id,
				Role = "Patient",
				HospitalId = user.HospitalId,
			});
		}
		}
}
