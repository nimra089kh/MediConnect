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


			builder.HasOne(u => u.Hospital)
					   .WithMany(h => h.Users)
					   .HasForeignKey(u => u.HospitalId)
					   .OnDelete(DeleteBehavior.NoAction);

			builder.HasMany(u => u.RefreshTokens)
				.WithOne(r => r.User)
				.HasForeignKey(r => r.UserId)
				.OnDelete(DeleteBehavior.Cascade);



		}
	}
}
