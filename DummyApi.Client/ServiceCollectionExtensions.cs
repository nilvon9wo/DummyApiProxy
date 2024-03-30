using Ardalis.GuardClauses;

using FluxSzerviz.DummyApi.Client.Users;
using FluxSzerviz.DummyApiProxy.Common;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluxSzerviz.DummyApi.Client;
public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddDummyApiClient(this IServiceCollection services, IConfiguration configuration)
	{
		_ = Guard.Against.Null(configuration);

		_ = services.Configure(configuration, "DummyApiSettings", out DummyApiSettings? dummyApiSettings)
				.AddTransient<UserClient>();

		_ = Guard.Against.Null(dummyApiSettings);
		_ = services.AddHttpClient<UserClient>()
			.ConfigureHttpClient((provider, client)
				=>
			{
				client.BaseAddress = dummyApiSettings.BaseUrl;
				client.DefaultRequestHeaders.Add("app-id", dummyApiSettings.ApiKey);
			});
		return services;
	}
}
