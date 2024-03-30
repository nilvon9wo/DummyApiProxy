using FluxSzerviz.DummyApi.Client.Exceptions;

using LanguageExt;

using System.Text.Json;

using static LanguageExt.Prelude;

namespace FluxSzerviz.DummyApi.Client.Users;

public class UserClient(DummyApiSettings dummyApiSettings, HttpClient httpClient)
{
	public virtual TryAsync<ICollection<User>> GetUsers(CancellationToken cancellationToken)
		=> TryAsync(async () 
			=> (await GetContent(httpClient, CreateRequest(dummyApiSettings), cancellationToken))
			.Match(
				Succ: content
					=> JsonSerializer.Deserialize<UserDataEnvelope>(content)?.Users
						?? throw new MalformedDataEnvelopeException("Malformed data received from Dummy API."),

				Fail: exception
					=> throw exception
			));

	private static HttpRequestMessage CreateRequest(DummyApiSettings dummyApiSettings)
		=> new(HttpMethod.Get, $"/data/v1/user?limit={dummyApiSettings.DefaultPageLimit}");

	private static TryAsync<string> GetContent(HttpClient httpClient, HttpRequestMessage request, CancellationToken cancellationToken)
		=> TryAsync(async () =>
		{
			HttpResponseMessage response = await httpClient.SendAsync(request, cancellationToken);
			return !response.IsSuccessStatusCode
				? throw new UsersUnavailableException($"Can't fetch users from {httpClient.BaseAddress}.")
				: await response.Content.ReadAsStringAsync();
		});

}
