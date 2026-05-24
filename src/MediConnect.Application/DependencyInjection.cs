using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using FluentValidation;
using MediatR;


namespace MediConnect.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(
			this IServiceCollection services)
		{
			services.AddMediatR(cfg =>
				cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

			services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());

			return services;
		}
	}
}
