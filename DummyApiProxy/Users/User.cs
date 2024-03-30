using Ardalis.GuardClauses;

using CommonUser = FluxSzerviz.DummyApiProxy.Common.Models.User;

namespace FluxSzerviz.DummyApiProxy.Host.Users;
public class User
{
	public required string FirstName { get; init; }
	public required string LastName { get; init; }

	public static User From(CommonUser user)
		=> new()
		{
			FirstName = Guard.Against.Null(user).FirstName,
			LastName = user.LastName,
		};
}
