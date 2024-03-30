using FluxSzerviz.DummyApi.Client.Users;
using FluxSzerviz.DummyApiProxy.Host;
using FluxSzerviz.DummyApiProxy.Host.Users;
using FluxSzerviz.TestUtilities.ResultFactories;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Moq;
using Moq.AutoMock;

using System.Net;
using DummyUser = FluxSzerviz.DummyApiProxy.Host.Users.User;

namespace FluxSzerviz.DummyApiProxy.Tests.Host;

public class DummyApiUserProxyTests
{
	[Fact]
	public async Task GetUsers_Returns_OkResult_With_Users()
	{
		// Arrange
		AutoMocker mocker = new();
		CancellationToken cancellationToken = new();
		IEnumerable<DummyUser> expectedUsers = new List<DummyUser>()
		{
			new() { FirstName = "John", LastName = "Doe" },
			new() { FirstName = "Jane", LastName = "Doe" }
		};

		Mock<ILogger<DummyApiUserProxy>> loggerMock = mocker.GetMock<ILogger<DummyApiUserProxy>>();
		Mock<UserProvider> userProviderMock = mocker.GetMock<UserProvider>();
		_ = userProviderMock
			.Setup(x => x.GetUsers(cancellationToken))
			.Returns(TryAsyncFactory.CreateSuccess(expectedUsers));

		DummyApiUserProxy proxyUnderTest = mocker.CreateInstance<DummyApiUserProxy>();
		Mock<HttpRequest> httpRequestMock = new();

		// Act
		IActionResult response = await proxyUnderTest.GetUsers(httpRequestMock.Object, cancellationToken);

		// Assert
		OkObjectResult okResult = Assert.IsType<OkObjectResult>(response);
		Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);

		UsersResponse usersResponse = Assert.IsType<UsersResponse>(okResult.Value);
		Assert.Equal(expectedUsers.Count(), usersResponse.Users.Count);
	}

	[Fact]
	public async Task GetUsers_Returns_InternalServerError_When_UserProvider_Fails()
	{
		// Arrange
		AutoMocker mocker = new();
		Mock<ILogger<DummyApiUserProxy>> loggerMock = mocker.GetMock<ILogger<DummyApiUserProxy>>();

		CancellationToken cancellationToken = new();

		Mock<UserProvider> userProviderMock = mocker.GetMock<UserProvider>();
		_ = userProviderMock
			.Setup(x => x.GetUsers(cancellationToken))
			.Returns(TryAsyncFactory.CreateFailure<IEnumerable<DummyUser>>(new UsersUnavailableException("Something went wrong.")));

		DummyApiUserProxy proxyUnderTest = mocker.CreateInstance<DummyApiUserProxy>();
		Mock<HttpRequest> httpRequestMock = new();

		// Act
		IActionResult response = await proxyUnderTest.GetUsers(httpRequestMock.Object, cancellationToken);

		// Assert
		ObjectResult result = Assert.IsType<ObjectResult>(response);
		Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
	}
}
