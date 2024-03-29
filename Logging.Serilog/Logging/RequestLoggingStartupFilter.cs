using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Serilog;

namespace FluxSzerviz.Logging.Serilog.Logging;

/// <summary>
/// Registers a middleware that runs early in the pipeline and logs incoming requests to serilog.
/// </summary>
/// <seealso cref="IStartupFilter" />
internal sealed class RequestLoggingStartupFilter : IStartupFilter
{
	public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next) => app =>
																						   {
																							   _ = app.UseMiddleware<DetectAzureSlotSwapMiddleware>();
																							   _ = app.UseSerilogRequestLogging();
																							   next(app);
																						   };
}
