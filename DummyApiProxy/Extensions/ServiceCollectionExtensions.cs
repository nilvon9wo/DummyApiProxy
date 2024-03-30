using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Serilog;
using Serilog.Configuration;
using Serilog.Core;

namespace FluxSzerviz.DummyApiProxy.Host.Extensions;

internal static class ServiceCollectionExtensions
{
	internal static IServiceCollection AddLoggingAndTelemetry(
			this IServiceCollection services,
			IConfiguration configuration
		)
	{
		LoggerConfiguration loggerConfiguration = new();
		LoggerSettingsConfiguration settingsConfiguration = loggerConfiguration.ReadFrom;
		LoggerConfiguration loggerConfiguration1 = settingsConfiguration.Configuration(configuration);
		Logger logger = loggerConfiguration1.CreateLogger();

		return services.AddApplicationInsights()
			.ConfigureLoggingOptions()
			.AddLogging(logBuilder => logBuilder.AddSerilog(logger));
	}

	private static IServiceCollection ConfigureLoggingOptions(this IServiceCollection services)
		=> services.Configure<LoggerFilterOptions>(options =>
	{
		LoggerFilterRule? toRemove = options.Rules
			.FirstOrDefault(rule => rule.ProviderName
			== "Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider");

		if (toRemove is not null)
		{
			_ = options.Rules.Remove(toRemove);
		}
	});

	private static IServiceCollection AddApplicationInsights(this IServiceCollection services)
		=> services.AddApplicationInsightsTelemetryWorkerService()
				.ConfigureFunctionsApplicationInsights();
}
