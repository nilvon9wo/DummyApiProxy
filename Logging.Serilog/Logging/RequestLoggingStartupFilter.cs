using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Serilog;

using System.Diagnostics.CodeAnalysis;

namespace FluxSzerviz.Logging.Serilog.Logging;

/// <summary>
/// Registers a middleware that runs early in the pipeline and logs incoming requests to serilog.
/// </summary>
/// <seealso cref="IStartupFilter" />
[SuppressMessage("Performance", "CA1812: Avoid uninstantiated internal classes", Justification = "False negative")]
internal sealed class RequestLoggingStartupFilter : IStartupFilter
{
	public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
		=> app
			=>
			{
				_ = app.UseMiddleware<DetectAzureSlotSwapMiddleware>();
				_ = app.UseSerilogRequestLogging();
				next(app);
			};
}
