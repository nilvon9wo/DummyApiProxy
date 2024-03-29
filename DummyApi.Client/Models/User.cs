using DummyApiProxy.Common.Enums;

using System.Text.Json.Serialization;
using CommonUser = DummyApiProxy.Common.Models.User;

namespace DummyApi.Client.Models;

#nullable disable
// We can't control what DummyApi sends us, and we don't want missing values to result in errors
public record User
{
	[JsonPropertyName("id")]
	public string Id { get; init; }

	[JsonPropertyName("title")]
	public string Title { get; init; }

	[JsonPropertyName("firstName")]
	public string FirstName { get; init; }

	[JsonPropertyName("lastName")]
	public string LastName { get; init; }

	[JsonPropertyName("picture")]
	public Uri PictureUrl { get; init; }

	public CommonUser ToCommon() 
		=> new()
		{
			Id = Id,
			Title = Title.ToTitle(),
			FirstName = FirstName,
			LastName = LastName,
			PictureUrl = PictureUrl,
		};
}
