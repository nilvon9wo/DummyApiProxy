using Ardalis.GuardClauses;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace FluxSzerviz.DummyApiProxy.Host.Extensions;

public static class ConfigurationBuilderExtensions
{
	public static IConfigurationBuilder ConfigureEnvironment(
			this IConfigurationBuilder configurationBuilder,
			HostBuilderContext hostContext,
			string[] args
		)
	{
		IHostEnvironment hostingEnvironment = Guard.Against.Null(hostContext).HostingEnvironment;
		string environmentName = hostingEnvironment.EnvironmentName;
		_ = configurationBuilder.SetBasePath(hostingEnvironment.ContentRootPath)
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			.AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
			.AddJsonFile("logging.json", optional: false, reloadOnChange: true)
			.AddJsonFile($"logging.{environmentName}.json", optional: true, reloadOnChange: true)
			.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
			.AddJsonFile($"appsettings.{environmentName}.local.json", optional: true, reloadOnChange: true)
			.AddJsonFile("logging.local.json", optional: true, reloadOnChange: true)
			.AddJsonFile($"logging.{environmentName}.local.json", optional: true, reloadOnChange: true);

		if (hostingEnvironment.IsDevelopment())
		{
			_ = configurationBuilder.AddUserSecrets<Program>(optional: true);
		}

		return configurationBuilder.AddEnvironmentVariables()
			.AddCommandLine(args);
	}
}
