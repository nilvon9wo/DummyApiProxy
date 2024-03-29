using Ardalis.GuardClauses;

using Serilog.Core;
using Serilog.Events;

namespace FluxSzerviz.Logging.Serilog.Logging;

public class EnvironmentVariableEnricher(string property, string variableName, string defaultValue) : ILogEventEnricher
{
	private readonly string _property = property;
	private readonly string _variableName = variableName;
	private readonly string _defaultValue = defaultValue;

	public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
	{
		string value = Environment.GetEnvironmentVariable(_variableName) ?? _defaultValue;
		if (value != null)
		{
			LogEventProperty property = Guard.Against.Null(propertyFactory)
				.CreateProperty(_property, value);

			Guard.Against.Null(logEvent)
				.AddPropertyIfAbsent(property);
		}
	}
}
