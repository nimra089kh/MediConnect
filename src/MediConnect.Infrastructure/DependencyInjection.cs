using MediConnect.Application.Interfaces.Repositories;
using MediConnect.Application.Interfaces.Services;
using MediConnect.Infrastructure.Services;
using MediConnect.Infrastructure.Persistence;
using MediConnect.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(
			this IServiceCollection services,
			IConfiguration configuration)
		{
		services.AddDbContext<ApplicationDbContext>(Options =>
			Options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

			services.AddScoped<IUnitOfWork , UnitOfWork> ();
			
			services.AddScoped<IJwtService, JwtService>();

			services.AddHttpClient();
			services.AddScoped<IAiService, OpenAiService>();

			return services;
		}

		
	}
}
