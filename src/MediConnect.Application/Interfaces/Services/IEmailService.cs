using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Application.Interfaces.Services
{
	public interface IEmailService
	{
		Task SendAppointmentConfirmationAsync(
			string toEmail,
			string patientName,
			string doctorName,
			DateTime appointmentDate
			);
		Task SendAppointmentCancellationAsync(
			string toEmail,
			string patientName,
			string reason
			);
		Task SendAppointmentReminderAsync(
				string toEmail,
			string patientName,
			string doctorName,
			DateTime appointmentDate
			)
			;
	}
}
