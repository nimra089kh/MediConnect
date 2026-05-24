using MediConnect.Application.Features.AI.DTOs;
using MediConnect.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediConnect.API.Controllers
{
		[ApiController]
		[Route("api/[controller]")]
		[Authorize]
		public class AiController : ControllerBase
		{
			private readonly IAiService _aiService;

			public AiController(IAiService aiService)
			{
				_aiService = aiService;
			}

			[HttpPost("symptom-check")]
			[Authorize(Roles = "Patient")]
			public async Task<IActionResult> CheckSymptoms(
				[FromBody] SymptomCheckerRequestDto request,
				CancellationToken cancellationToken)
			{
				if (string.IsNullOrWhiteSpace(request.Symptoms))
					return BadRequest(new { error = "Symptoms are required." });

				if (request.Symptoms.Length > 500)
					return BadRequest(new { error = "Symptoms too long. Max 500 characters." });

				var result = await _aiService.AnalyzeSymptomsAsync(
					request.Symptoms,
					cancellationToken);

				return Ok(result);
			}
		}
}
