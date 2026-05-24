using System;
using System.Collections.Generic;
using System.Text;

namespace MediConnect.Domain.Enums
{
	public enum AppointmentStatus
	{
		Pending = 1,
		Confirmed = 2,
		Rejected = 3,
		Completed = 4,
		Canceled = 5,
		Rescheduled = 6,
		Noshow = 7,
	}
}
