using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Application.Features.Auth.Commands
{
	public class RegisterPatientValidator : AbstractValidator<RegisterPatientCommand>
	{
		public RegisterPatientValidator() {
			RuleFor(x => x.FirstName)
					.NotEmpty().WithMessage("First name is required")
					.MaximumLength(50).WithMessage("First name cannot exceed 50 characters");
			RuleFor(x => x.LastName)
					.NotEmpty().WithMessage("Last name is required")
					.MaximumLength(50).WithMessage("Last name cannot exceed 50 characters");
			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("Email is required")
				.EmailAddress().WithMessage("Invalid email Format");
			RuleFor(x => x.Password)
				.NotEmpty().WithMessage("Password is required")
				.MinimumLength(8).WithMessage("Password must be atleast 8 characters")
				.Matches("[A-Z]").WithMessage("Password must have at least one uppercase letter")
				.Matches("[0-9]").WithMessage("Password must have at least one number");
			RuleFor(x => x.HospitalId)
		   .NotEmpty().WithMessage("Hospital is required.");
		}

	}
}
