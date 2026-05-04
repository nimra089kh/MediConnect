using MediConnect.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Application.Features.Auth.DTOs
{
	public class LoginRequestDtos
	{
		public string Email { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
	}
	public class AuthResponseDto
	{
		public string AccessToken { get; set; } = string.Empty;
		public string RefreshToken { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Role { get; set; } = string.Empty;
		public Guid UserId { get; set; }
		public Guid HospitalId { get; set; }
	}

	public class RegisterPatientDto
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public Guid HospitalId { get; set; }


		// Optional 
		public DateOnly? DateOfBirth { get; set; }
		public Gender? Gender { get; set; }
		public string? Phone { get; set; }

	}
}
