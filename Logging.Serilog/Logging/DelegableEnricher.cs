using Ardalis.GuardClauses;

using Serilog.Core;
using Serilog.Events;

namespace FluxSzerviz.Logging.Serilog.Logging;

public class DelegableEnricher(
		string property,
		Func<object> factory,
		bool logNull = false,
		bool destructureObjects = false
	) : ILogEventEnricher
{
	private readonly string _property = Guard.Against.Null(property);
	private readonly Func<object> _factory = Guard.Against.Null(factory);
	private readonly bool _logNull = logNull;
	private readonly bool _destructureObjects = destructureObjects;

	public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
	{
		object value = _factory();
		if (_logNull || value != null)
		{
			LogEventProperty property = Guard.Against.Null(propertyFactory)
				.CreateProperty(_property, value, _destructureObjects);

			Guard.Against.Null(logEvent)
				.AddPropertyIfAbsent(property);
		}
	}
}
