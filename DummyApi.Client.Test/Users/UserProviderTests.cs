using System.Net;
using System.Text.Json;

using DummyApi.Client.Configs;
using DummyApi.Client.Models;
using DummyApi.Client.Users;

using LanguageExt;

using Moq;

namespace DummyApi.Client.Tests;

public class UserProviderTests
{
	[Fact]
	public async Task GetUsers_Returns_Collection_Of_Users()
	{
		// Arrange
		DummyApiSettings dummyApiSettings = CreateDummyApiSettings();
		List<User> users = [new User { Id = "1", FirstName = "John", LastName = "Doe" }];
		string content = JsonSerializer.Serialize(new UserDataEnvelope()
		{
			Users = users
		});
		using HttpResponseMessage message = new()
		{
			StatusCode = HttpStatusCode.OK,
			Content = new StringContent(content)
		};
		Mock<HttpClient> httpClientMock = CreateMockHttpClient(message);

		CancellationToken token = default;
		UserProvider providerUnderTest = new(dummyApiSettings, httpClientMock.Object);

		// Act
		Try<ICollection<User>> result = await providerUnderTest.GetUsers(token);

		// Assert
		Assert.True(result.IsSucc());
		_ = result.IfSucc(actualUsers => Assert.Equal(users, actualUsers));
	}

	[Fact]
	public async Task GetUsers_Returns_Empty_Collection_When_Content_Is_Null()
	{
		// Arrange
		DummyApiSettings dummyApiSettings = CreateDummyApiSettings();
		using HttpResponseMessage message = new()
		{
			StatusCode = HttpStatusCode.OK,
			Content = null
		};
		Mock<HttpClient> httpClientMock = CreateMockHttpClient(message);

		CancellationToken token = default;
		UserProvider providerUnderTest = new(dummyApiSettings, httpClientMock.Object);

		// Act
		Try<ICollection<User>> result = await providerUnderTest.GetUsers(token);

		// Assert
		Assert.True(result.IsFail());
	}

	[Fact]
	public async Task GetUsers_Returns_Empty_Collection_When_Deserialization_Fails()
	{
		// Arrange
		DummyApiSettings dummyApiSettings = CreateDummyApiSettings();
		string invalidJson = "Invalid JSON";
		using HttpResponseMessage message = new()
		{
			StatusCode = HttpStatusCode.OK,
			Content = new StringContent(invalidJson)
		};
		Mock<HttpClient> httpClientMock = CreateMockHttpClient(message);

		CancellationToken token = default;
		UserProvider providerUnderTest = new(dummyApiSettings, httpClientMock.Object);

		// Act
		Try<ICollection<User>> result = await providerUnderTest.GetUsers(token);

		// Assert
		Assert.True(result.IsFail());
	}

	[Fact]
	public async Task GetUsers_Returns_Empty_Collection_When_Response_Is_Not_Successful()
	{
		// Arrange
		DummyApiSettings dummyApiSettings = CreateDummyApiSettings();
		using HttpResponseMessage message = new()
		{
			StatusCode = HttpStatusCode.BadRequest,
			Content = new StringContent("Error message")
		};
		Mock<HttpClient> httpClientMock = CreateMockHttpClient(message);

		CancellationToken token = default;
		UserProvider providerUnderTest = new(dummyApiSettings, httpClientMock.Object);

		// Act
		Try<ICollection<User>> result = await providerUnderTest.GetUsers(token);

		// Assert
		Assert.True(result.IsSucc());
		_ = result.IfSucc(actualUsers => Assert.Equal([], actualUsers));
	}

	private static DummyApiSettings CreateDummyApiSettings() 
		=> new()
			{
				ApiKey = Guid.NewGuid().ToString(),
				BaseUrl = new Uri("http://www.example.com"),
				DefaultPageLimit = 10
			};

	private static Mock<HttpClient> CreateMockHttpClient(HttpResponseMessage message)
	{
		Mock<HttpClient> httpClientMock = new();
		httpClientMock.Object.BaseAddress = new Uri("http://www.example.com");
		_ = httpClientMock
			.Setup(client => client.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(message);
		return httpClientMock;
	}
}
