using Destructurama;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;

namespace FluxSzerviz.Logging.Serilog.Logging;

public static class LoggingExtensions
{
	/// <summary>
	/// Configures serilog as a logger.
	/// </summary>
	/// <remarks>
	/// Required dependencies: Serilog, Serilog.AspNetCore, Destructurama.JsonNet; optional dependencies: sinks based on configuration.
	/// </remarks>
	public static T UseLogging<T>(this T builder) where T : IHostBuilder
		=> (T)builder
			.UseSerilog((hostingContext, loggerConfig)
				=> _ = loggerConfig
					.ReadFrom.Configuration(hostingContext.Configuration)
					.Destructure.JsonNetTypes()
					.Enrich.FromLogContext()
					.Enrich.WithAzureWebAppsSiteName()
					.Enrich.WithAzureWebAppsSlotName()
					.Enrich.WithAspNetCoreEnvironment()
			)
			.ConfigureServices((hostingContext, services)
				=> _ = services.AddTransient<IStartupFilter, RequestLoggingStartupFilter>()
			);
}
