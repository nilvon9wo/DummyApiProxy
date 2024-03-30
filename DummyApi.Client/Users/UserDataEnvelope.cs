using System.Text.Json.Serialization;

namespace FluxSzerviz.DummyApi.Client.Users;

#nullable disable
// We can't control what DummyApi sends us, and we don't want missing values to result in errors

public record UserDataEnvelope
{
	[JsonPropertyName("data")]
	public ICollection<User> Users { get; init; }

	[JsonPropertyName("total")]
	public int CountAtSource { get; init; }

	[JsonPropertyName("page")]
	public int PageNumber { get; init; }

	[JsonPropertyName("limit")]
	public int MaxRecordsOnPage { get; init; }
}
