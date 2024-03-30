using FluxSzerviz.DummyApiProxy.Common.Titles;

using CommonUser = FluxSzerviz.DummyApiProxy.Common.Models.User;
using OutboundUser = FluxSzerviz.DummyApiProxy.Host.Users.User;
namespace FluxSzerviz.DummyApiProxy.Host.Test.Users;

public class UserTests
{
	[Fact]
	public void From_Returns_User_With_Valid_CommonUser()
	{
		// Arrange
		CommonUser commonUser = new()
		{
			Id = Guid.NewGuid().ToString(),
			FirstName = "John",
			LastName = "Doe",
			Title = Title.None,
			PictureUrl = new("http://www.example.com/pictures/image.jpg")

		};

		// Act
		OutboundUser user = OutboundUser.From(commonUser);

		// Assert
		Assert.NotNull(user);
		Assert.Equal("John", user.FirstName);
		Assert.Equal("Doe", user.LastName);
	}

	[Fact]
	public void From_Throws_Exception_When_CommonUser_Is_Null()
	{
		// Arrange
		CommonUser? commonUser = null;

		// Act & Assert
		_ = Assert.Throws<ArgumentNullException>(() => OutboundUser.From(commonUser!));
	}
}
