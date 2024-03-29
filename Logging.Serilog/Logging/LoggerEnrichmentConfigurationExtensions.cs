using Ardalis.GuardClauses;

using Serilog;
using Serilog.Configuration;

namespace FluxSzerviz.Logging.Serilog.Logging;

public static class LoggerEnrichmentConfigurationExtensions
{
	public static LoggerConfiguration WithAzureWebAppsHostName(this LoggerEnrichmentConfiguration enrichmentConfiguration)
		=> enrichmentConfiguration.WithEnvironmentVariable("AzureWebAppsHostName", "WEBSITE_HOSTNAME", "localhost");

	public static LoggerConfiguration WithAzureWebAppsSiteName(this LoggerEnrichmentConfiguration enrichmentConfiguration)
		=> enrichmentConfiguration.WithEnvironmentVariable("AzureWebAppsSiteName", "WEBSITE_SITE_NAME", "LOCAL");

	public static LoggerConfiguration WithAzureWebAppsSlotName(this LoggerEnrichmentConfiguration enrichmentConfiguration)
		=> Guard.Against.Null(enrichmentConfiguration)
			.With(
			new DelegableEnricher("AzureWebAppsSlotName", () =>
			{
				string? hostName = Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME");
				string? siteName = Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME");

				if (hostName != null && siteName != null && hostName.StartsWith(siteName, StringComparison.OrdinalIgnoreCase))
				{
					int idx = hostName.IndexOf('.', siteName.Length);
					if (idx >= 0)
					{
						hostName = hostName[..idx];
					}
				}

				return hostName!;
			}));

	public static LoggerConfiguration WithAspNetCoreEnvironment(this LoggerEnrichmentConfiguration enrichmentConfiguration)
		=> enrichmentConfiguration.WithEnvironmentVariable("AspNetCoreEnvironment", "ASPNETCORE_ENVIRONMENT");

	private static LoggerConfiguration WithEnvironmentVariable(
			this LoggerEnrichmentConfiguration enrichmentConfiguration,
			string property,
			string environmentVariable,
			string? defaultValue = null
		)
	{
		_ = Guard.Against.Null(enrichmentConfiguration);
		_ = Guard.Against.Null(environmentVariable);
		return enrichmentConfiguration
					.With(new EnvironmentVariableEnricher(property, environmentVariable, defaultValue!));
	}
}
