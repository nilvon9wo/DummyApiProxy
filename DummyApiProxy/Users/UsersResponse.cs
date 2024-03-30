namespace FluxSzerviz.DummyApiProxy.Host.Users;

public record UsersResponse
{
	public required ICollection<User> Users { get; init; }
	public string? Error { get; init; }

	internal static UsersResponse From(ICollection<User> users)
		=> new()
		{
			Users = users
		};

	internal static UsersResponse From(Exception exception)
		=> new()
		{
			Users = [],
			Error = exception.Message
		};
}