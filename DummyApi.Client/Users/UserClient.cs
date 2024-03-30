using LanguageExt;

using System.Text.Json;

using static LanguageExt.Prelude;

namespace FluxSzerviz.DummyApi.Client.Users;

public class UserClient(DummyApiSettings dummyApiSettings, HttpClient httpClient)
{
	public virtual TryAsync<ICollection<User>> GetUsers(CancellationToken cancellationToken)
		=> TryAsync(async () =>
		{
			using HttpRequestMessage request = CreateRequest(dummyApiSettings);
			return (await GetContent(httpClient, request, cancellationToken))
				.Match(
					Succ: content =>
						JsonSerializer.Deserialize<UserDataEnvelope>(content)?.Users ?? [],
					Fail: []
				);
		});

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
