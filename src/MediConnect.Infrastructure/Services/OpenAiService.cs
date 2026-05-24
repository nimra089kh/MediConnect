using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MediConnect.Application.Interfaces.Services;
using Microsoft.Extensions.Configuration;


namespace MediConnect.Infrastructure.Services
{
	public class OpenAiService : IAiService
	{
		private readonly HttpClient _httpClient;
		private readonly string _apiKey;

		public OpenAiService(
			IConfiguration configuration,
			IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient();
			_apiKey = configuration["OpenAI:ApiKey"]
				?? throw new InvalidOperationException("OpenAI ApiKey missing.");
		}

		public async Task<SymptomAnalysisResult> AnalyzeSymptomsAsync(
			string symptoms,
			CancellationToken cancellationToken = default)
		{
			var systemPrompt = """
            You are a medical AI assistant for MediConnect hospital system.
            Analyze patient symptoms and provide:
            1. List of possible conditions (max 3)
            2. Severity: Mild, Moderate, or Severe
            3. Urgency: Can wait, See doctor soon, Emergency
            4. Basic advice
            
            IMPORTANT: Always remind that this is not a diagnosis.
            Respond in JSON format only:
            {
                "possibleConditions": ["condition1", "condition2"],
                "severity": "Mild/Moderate/Severe",
                "urgency": "Can wait/See doctor soon/Emergency",
                "advice": "basic advice here",
                "disclaimer": "disclaimer here"
            }
            """;

			var requestBody = new
			{
				model = "llama-3.3-70b-versatile",
				messages = new[]
				{
				new { role = "system", content = systemPrompt },
				new { role = "user",   content = $"Patient symptoms: {symptoms}" }
			},
				temperature = 0.3,
				max_tokens = 500
			};

			var json = JsonSerializer.Serialize(requestBody);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			_httpClient.DefaultRequestHeaders.Clear();
			_httpClient.DefaultRequestHeaders
				.Add("Authorization", $"Bearer {_apiKey}");

			var response = await _httpClient.PostAsync(
				"https://api.groq.com/openai/v1/chat/completions",
				content,
				cancellationToken);

			response.EnsureSuccessStatusCode();

			if (!response.IsSuccessStatusCode)
			{
				var errorBody = await response.Content
					.ReadAsStringAsync(cancellationToken);
				Console.WriteLine($"=== GROQ ERROR ===");
				Console.WriteLine($"Status: {response.StatusCode}");
				Console.WriteLine($"Body: {errorBody}");
				Console.WriteLine($"=================");
			}

			response.EnsureSuccessStatusCode();

			var responseJson = await response.Content
				.ReadAsStringAsync(cancellationToken);

			using var doc = JsonDocument.Parse(responseJson);

			var messageContent = doc.RootElement
				.GetProperty("choices")[0]
				.GetProperty("message")
				.GetProperty("content")
				.GetString();

			// Parse AI response
			var result = JsonSerializer.Deserialize<SymptomAnalysisResult>(
				messageContent!,
				new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});

			return result ?? new SymptomAnalysisResult
			{
				Advice = "Unable to analyze symptoms. Please consult a doctor.",
				Disclaimer = "This is not a medical diagnosis."
			};
		}
	}
}
