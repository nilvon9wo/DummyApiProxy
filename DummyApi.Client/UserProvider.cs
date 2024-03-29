using DummyApi.Client.Configs;
using DummyApi.Client.Models;

using System.Text.Json;

namespace DummyApi.Client;

public class UserProvider(DummyApiSettings dummyApiSettings, HttpClient httpClient)
{
	public async Task<ICollection<User>> GetUsers()
	{
		using HttpRequestMessage request = CreateRequest(dummyApiSettings);
		string content = await GetContent(httpClient, request);
		return JsonSerializer.Deserialize<UserDataEnvelope>(content)?.Users
			?? new();
	}

	private static HttpRequestMessage CreateRequest(DummyApiSettings dummyApiSettings) 
		=> new(HttpMethod.Get, $"/data/v1/user?limit={dummyApiSettings.DefaultPageLimit}");

	private static async Task<string> GetContent(HttpClient httpClient, HttpRequestMessage request)
	{
		HttpResponseMessage response = await httpClient.SendAsync(request);
		_ = response.EnsureSuccessStatusCode();
		return await response.Content.ReadAsStringAsync();
	}
}
