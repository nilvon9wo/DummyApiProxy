namespace FluxSzerviz.DummyApi.Client.Users;

public class UsersUnavailableException : Exception
{
	public UsersUnavailableException(string message) : base(message)
	{
	}

	public UsersUnavailableException(string message, Exception innerException) : base(message, innerException)
	{
	}

	public UsersUnavailableException()
	{
	}
}
