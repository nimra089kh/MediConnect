using MediConnect.Application.Interfaces.Services;
using MediConnect.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace MediConnect.Infrastructure.Services
{
	public class JwtService : IJwtService

	{
		private readonly IConfiguration _configuration;

		public JwtService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string GenerateAccessToken(User user, string role)
		{
			var claims = new[]
			{
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new Claim(ClaimTypes.Email, user.Email),
			new Claim(ClaimTypes.Role, role),
			new Claim("HospitalId", user.HospitalId.ToString())
		};

			var key = new SymmetricSecurityKey(
				Encoding.UTF8
				.GetBytes(_configuration["Jwt:SecretKey"]));

			var credentials = new SigningCredentials
				(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _configuration["Jwt:Issuer"],
				audience: _configuration["Jwt:Audience"],
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(15),
				signingCredentials: credentials
			);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		public string GenerateRefreshToken()
		{
			var randomBytes = new byte[64];
			using var rng = RandomNumberGenerator.Create();
				rng.GetBytes(randomBytes);
				return Convert.ToBase64String(randomBytes);
			
		}

		public Guid? GetUserIdFromToken(string token)
		{
			try
			{
				var Handler = new JwtSecurityTokenHandler();
				var jsonToken = Handler.ReadToken(token) as JwtSecurityToken;
				var userIdClaim = jsonToken?.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

				return Guid.TryParse(userIdClaim, out var userId)
					? userId
					: null
				;
			}
			catch
			{
				return null;
			}
		}
	}
}
