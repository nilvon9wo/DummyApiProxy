
namespace FluxSzerviz.DummyApiProxy.Host.Models;

public record UsersResponse
{
	public required ICollection<User> Users { get; init; }

	internal static UsersResponse From(ICollection<User> users) 
		=> new ()
		{
			Users = users
		};
}