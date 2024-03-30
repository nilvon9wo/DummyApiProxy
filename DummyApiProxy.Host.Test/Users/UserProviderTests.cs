using FluxSzerviz.DummyApi.Client.Users;
using FluxSzerviz.TestUtilities.ResultFactories;

using Moq;
using Moq.AutoMock;

using DummyUser = FluxSzerviz.DummyApi.Client.Users.User;
using OutboundUser = FluxSzerviz.DummyApiProxy.Host.Users.User;
using UserProvider = FluxSzerviz.DummyApiProxy.Host.Users.UserProvider;

namespace FluxSzerviz.DummyApiProxy.Host.Test.Users;

public class UserProviderTests
{
	[Fact]
	public async Task GetUsers_Returns_Collection_Of_OutboundUsers_When_UserProvider_Succeeds()
	{
		// Arrange
		CancellationToken cancellationToken = new();
		ICollection<DummyUser> dummyUsers =
		[
			new() { Id = "1", FirstName = "John", LastName = "Cash", Title = "Mr", PictureUrl = new("https://www.example.com/picture/john.jpg") },
			new() { Id = "2", FirstName = "Jane", LastName = "Joplin", Title = "Ms", PictureUrl = new("https://www.example.com/picture/jane.jpg") }
		];

		AutoMocker mocker = new();
		Mock<UserClient> userClientMock = mocker.GetMock<UserClient>();
		_ = userClientMock
			.Setup(x => x.GetUsers(cancellationToken))
			.Returns(() => TryAsyncFactory.CreateSuccess(dummyUsers));

		UserProvider userProviderUnderTest = mocker.CreateInstance<UserProvider>();

		// Act
		ICollection<OutboundUser> result = await userProviderUnderTest.GetUsers(cancellationToken);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(dummyUsers.Count, result.Count);
		Assert.Equal(dummyUsers.Select(u => u.FirstName), result.Select(u => u.FirstName));
		Assert.Equal(dummyUsers.Select(u => u.LastName), result.Select(u => u.LastName));
	}

	[Fact]
	public async Task GetUsers_Returns_Empty_Collection_Of_OutboundUsers_When_UserProvider_Fails()
	{
		// Arrange
		CancellationToken cancellationToken = new();

		AutoMocker mocker = new();
		Mock<UserClient> userClientMock = mocker.GetMock<UserClient>();
		_ = userClientMock
			.Setup(x => x.GetUsers(cancellationToken))
			.Returns(() => TryAsyncFactory.CreateFailure<ICollection<DummyUser>>(new UsersUnavailableException("Something went wrong.")));

		UserProvider userProviderUnderTest = mocker.CreateInstance<UserProvider>();

		// Act
		ICollection<OutboundUser> result = await userProviderUnderTest.GetUsers(cancellationToken);

		// Assert
		Assert.NotNull(result);
		Assert.Empty(result);
	}
}
