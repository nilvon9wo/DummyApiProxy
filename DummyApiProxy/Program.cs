using FluxSzerviz.DummyApi.Client;
using FluxSzerviz.DummyApiProxy.Host.Extensions;
using FluxSzerviz.DummyApiProxy.Host.Users;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

new HostBuilder()
	.ConfigureAppConfiguration((hostContext, config)
			=> config.ConfigureEnvironment(hostContext, args))
	.ConfigureFunctionsWebApplication()
	.ConfigureServices((context, services) =>
	{
		IConfiguration configuration = context.Configuration;
		_ = services.AddLoggingAndTelemetry(configuration)
			.AddDummyApiClient(configuration)
			.AddTransient<UserProvider>();
	})
	.Build()
	.Run();
