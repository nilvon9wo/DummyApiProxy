using FluxSzerviz.DummyApiProxy.Common.Titles;

namespace FluxSzerviz.DummyApiProxy.Common.Models;

public record User
{
	public required string FirstName { get; init; }
	public required string Id { get; init; }
	public required Title Title { get; init; }
	public required string LastName { get; init; }
	public required Uri PictureUrl { get; init; }
}
