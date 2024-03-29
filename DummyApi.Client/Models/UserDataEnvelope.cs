using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace DummyApi.Client.Models;

#nullable disable
// We can't control what DummyApi sends us, and we don't want missing values to result in errors

public record UserDataEnvelope
{
	[JsonPropertyName("data")]
	public Collection<User> Users { get; init; }

	[JsonPropertyName("total")]
	public int CountAtSource { get; init; }

	[JsonPropertyName("page")]
	public int PageNumber { get; init; }

	[JsonPropertyName("limit")]
	public int MaxRecordsOnPage { get; init; }
}
