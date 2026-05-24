using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediConnect.Application.Interfaces.Services
{
	public interface IAiService
	{
		Task<SymptomAnalysisResult> AnalyzeSymptomsAsync(
			string symptoms,
			CancellationToken cancellationToken = default);
	}

	public class SymptomAnalysisResult
	{
		public List<string> PossibleConditions { get; set; } = new();
		public string Severity { get; set; } = string.Empty;
		public string Urgency { get; set; } = string.Empty;
		public string Advice { get; set; } = string.Empty;
		public string Disclaimer { get; set; } = string.Empty;
	}
}
