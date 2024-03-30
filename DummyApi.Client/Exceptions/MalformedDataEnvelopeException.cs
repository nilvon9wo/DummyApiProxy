namespace FluxSzerviz.DummyApi.Client.Exceptions;

public class MalformedDataEnvelopeException : Exception
{
	public MalformedDataEnvelopeException(string message) : base(message)
	{
	}

	public MalformedDataEnvelopeException(string message, Exception innerException) : base(message, innerException)
	{
	}

	public MalformedDataEnvelopeException()
	{
	}
}
