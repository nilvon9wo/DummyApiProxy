using DummyApiProxy.Common.Enums;
using CommonUser = DummyApiProxy.Common.Models.User;
namespace DummyApi.Client.Models.Tests;

public class UserTests
{
	[Fact]
	public void ToCommon_Converts_User_To_CommonUser()
	{
		// Arrange
		User user = new()
		{
			Id = "1",
			Title = "Mr",
			FirstName = "John",
			LastName = "Doe",
			PictureUrl = new Uri("http://example.com/picture.jpg")
		};

		// Act
		CommonUser commonUser = user.ToCommon();

		// Assert
		Assert.NotNull(commonUser);
		Assert.Equal(user.Id, commonUser.Id);
		Assert.Equal(Title.Mr, commonUser.Title);
		Assert.Equal(user.FirstName, commonUser.FirstName);
		Assert.Equal(user.LastName, commonUser.LastName);
		Assert.Equal(user.PictureUrl, commonUser.PictureUrl);
	}

	[Fact]
	public void ToCommon_Null_PictureUrl_Results_In_Null_PictureUrl_In_CommonUser()
	{
		// Arrange
		User user = new()
		{
			Id = "1",
			Title = "Mr",
			FirstName = "John",
			LastName = "Doe",
			PictureUrl = null 
		};

		// Act
		CommonUser commonUser = user.ToCommon();

		// Assert
		Assert.NotNull(commonUser);
		Assert.Equal(user.Id, commonUser.Id);
		Assert.Equal(Title.Mr, commonUser.Title);
		Assert.Equal(user.FirstName, commonUser.FirstName);
		Assert.Equal(user.LastName, commonUser.LastName);
		Assert.Null(commonUser.PictureUrl);
	}

	[Fact]
	public void ToCommon_Whitespace_Title_Results_In_None_Title_In_CommonUser()
	{
		// Arrange
		User user = new()
		{
			Id = "1",
			Title = "   ",
			FirstName = "John",
			LastName = "Doe",
			PictureUrl = new Uri("http://example.com/picture.jpg")
		};

		// Act
		CommonUser commonUser = user.ToCommon();

		// Assert
		Assert.NotNull(commonUser);
		Assert.Equal(user.Id, commonUser.Id);
		Assert.Equal(Title.None, commonUser.Title);
		Assert.Equal(user.FirstName, commonUser.FirstName);
		Assert.Equal(user.LastName, commonUser.LastName);
		Assert.Equal(user.PictureUrl, commonUser.PictureUrl);
	}

	[Fact]
	public void ToCommon_Null_Title_Results_In_None_Title_In_CommonUser()
	{
		// Arrange
		User user = new()
		{
			Id = "1",
			Title = null, 
			FirstName = "John",
			LastName = "Doe",
			PictureUrl = new Uri("http://example.com/picture.jpg")
		};

		// Act
		CommonUser commonUser = user.ToCommon();

		// Assert
		Assert.NotNull(commonUser);
		Assert.Equal(user.Id, commonUser.Id);
		Assert.Equal(Title.None, commonUser.Title);
		Assert.Equal(user.FirstName, commonUser.FirstName);
		Assert.Equal(user.LastName, commonUser.LastName);
		Assert.Equal(user.PictureUrl, commonUser.PictureUrl);
	}
}
