using MediConnect.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Application.Interfaces.Services
{
	public interface IJwtService
	{
		string GenerateAccessToken(User user, string role);
		string GenerateRefreshToken();
		Guid? GetUserIdFromToken(string token);
	}
}
