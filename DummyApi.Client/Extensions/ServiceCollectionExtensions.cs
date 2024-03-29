using Ardalis.GuardClauses;

using DummyApi.Client.Configs;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DummyApi.Client.Extensions;
public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddDummyApiClient(this IServiceCollection services, IConfiguration configuration)
	{
		_ = Guard.Against.Null(configuration);

		_ = services.Configure(configuration, "DummyApiSettings", out DummyApiSettings? dummyApiSettings)
				.AddTransient<UserProvider>();

		_ = Guard.Against.Null(dummyApiSettings);
		_ = services.AddHttpClient<UserProvider>()
			.ConfigureHttpClient((provider, client)
				=>
			{
				client.BaseAddress = dummyApiSettings.BaseUrl;
				client.DefaultRequestHeaders.Add("app-id", dummyApiSettings.ApiKey);
			});
		return services;
	}
}
