using MediConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Infrastructure.Persistence.Configurations
{
	public class RefundConfiguration : IEntityTypeConfiguration<Refund>
	{
		public void Configure(EntityTypeBuilder<Refund> builder)
		{
			builder.HasKey(r => r.Id);

			builder.Property(r => r.Amount)
				.HasColumnType("decimal(10,2)");
		}
	}
	}
