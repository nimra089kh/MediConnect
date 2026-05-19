using System.Net;
using System.Text.Json;

namespace MediConnect.API.Middleware
{
	public class ExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _Next;
		private readonly ILogger<ExceptionHandlingMiddleware> _Logger;

		public ExceptionHandlingMiddleware(RequestDelegate Next, ILogger<ExceptionHandlingMiddleware> Logger)
		{
			_Next = Next;
			_Logger = Logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _Next(context);
			}
			catch (Exception ex)
			{
				_Logger.LogError(ex, "An unhandled exception occurred while processing the request.");
				await HandleExceptionAsync(context, ex);
			}
		}

		private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			var response = new
			{
				error = "An unexpected error occurred. Please try again later.",
				Details = exception.Message
			};
			await context.Response.WriteAsync(JsonSerializer.Serialize(response));
		}
	}
}
