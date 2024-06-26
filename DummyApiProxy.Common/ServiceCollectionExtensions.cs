﻿using Ardalis.GuardClauses;

using FluxSzerviz.DummyApiProxy.Common;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using System.Diagnostics.CodeAnalysis;

namespace FluxSzerviz.DummyApiProxy.Common;
public static class ServiceCollectionExtensions
{
	[SuppressMessage("Design", "CA1021:Avoid out parameters", Justification = "Allows for more fluid programming.")]
	public static IServiceCollection Configure<T>(
			this IServiceCollection services,
			IConfiguration configuration,
			string configName,
			out T? config
		)
		where T : class
	{
		config = (T?)Activator.CreateInstance(typeof(T));
		IConfigurationSection configSection = Guard.Against.Null(configuration)
			.GetSection(configName);

		configSection.Bind(config);
		return services.Configure<T>(configSection)
			.AddTransient(provider => provider.GetRequiredService<IOptions<T>>().Value);
	}
}
