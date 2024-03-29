using Ardalis.GuardClauses;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace FluxSzerviz.Logging.Serilog.Logging;

public class DetectAzureSlotSwapMiddleware(RequestDelegate next)
{
	private readonly RequestDelegate _next = Guard.Against.Null(next);

	public async Task InvokeAsync(HttpContext context)
	{
		_ = Guard.Against.Null(context);
		IHeaderDictionary headers = context.Request.Headers;
		if (headers.TryGetValue("WAS-DEFAULT-HOSTNAME", out StringValues values))
		{
			Environment.SetEnvironmentVariable("WEBSITE_HOSTNAME", values, EnvironmentVariableTarget.Process);
		}

		await _next(context);
	}
}
