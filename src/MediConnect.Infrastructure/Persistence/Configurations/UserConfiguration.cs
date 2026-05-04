using MediConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Infrastructure.Persistence.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(u => u.Id);
			builder.Property(u => u.Email)
				.IsRequired().HasMaxLength(100);
			builder.HasIndex(u => u.Email)
				.IsUnique();
			builder.Property(u => u.PasswordHash)
				.IsRequired();
			builder.HasOne(u => u.Doctor)
				.WithOne(d => d.User)
				.HasForeignKey<Doctor>(d => d.UserId);
			builder.HasOne(u => u.Patient)
				.WithOne(p => p.User)
				.HasForeignKey<Patient>(p => p.UserId);
			builder.HasMany(u => u.RefreshTokens)
				.WithOne(rt => rt.User)
				.HasForeignKey(rt => rt.UserId);


		}
	}
}
