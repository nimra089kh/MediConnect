using MediConnect.Application;
using MediConnect.Infrastructure;
using MediConnect.Infrastructure.Persistence;
using MediConnect.Infrastructure.Persistence.Seeds;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.Converters
			.Add(new MediConnect.API.Converters.TimeOnlyJsonConverter());
		options.JsonSerializerOptions.Converters
			.Add(new MediConnect.API.Converters.DateOnlyJsonConverter());
		options.JsonSerializerOptions.Converters
			.Add(new System.Text.Json.Serialization
				.JsonStringEnumConverter());
	});
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "MediConnect API",
		Version = "v1"
	});

	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "Bearer {token} format mein daalo. Example: Bearer eyJhbGci..."
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id   = "Bearer"
				}
			},
			Array.Empty<string>()
		}
	});
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var jwtKey = builder.Configuration["Jwt:SecretKey"]
	?? throw new InvalidOperationException("JWT SecretKey missing.");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			ValidAudience = builder.Configuration["Jwt:Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(jwtKey))
		};
	});

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
	var context = scope.ServiceProvider
		.GetRequiredService<ApplicationDbContext>();
	await DatabaseSeeder.SeedAsync(context);
}

app.Run();