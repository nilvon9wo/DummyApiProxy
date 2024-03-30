using FluxSzerviz.DummyApiProxy.Common.Titles;

namespace FluxSzerviz.DummyApiProxy.Common.Test.Titles;

public class TitleExtensionsTests
{
	[Theory]
	[InlineData("Mr", Title.Mr)]
	[InlineData("Ms", Title.Ms)]
	[InlineData("Mrs", Title.Mrs)]
	[InlineData("Miss", Title.Miss)]
	[InlineData("Dr", Title.Dr)]
	[InlineData("", Title.None)]
	[InlineData("Unknown", Title.None)] // Test case for unknown title string
	public void ToTitle_Returns_Correct_Title(string titleString, Title expectedTitle)
	{
		// Arrange
		// Nothing to do here.

		// Act
		Title actualTitle = titleString.ToTitle();

		// Assert
		Assert.Equal(expectedTitle, actualTitle);
	}

	[Theory]
	[InlineData(Title.Mr, "Mr")]
	[InlineData(Title.Ms, "Ms")]
	[InlineData(Title.Mrs, "Mrs")]
	[InlineData(Title.Miss, "Miss")]
	[InlineData(Title.Dr, "Dr")]
	[InlineData(Title.None, "")]
	public void ToDescription_Returns_Correct_Description(Title title, string expectedDescription)
	{
		// Arrange
		// Nothing to do here.

		// Act
		string actualDescription = title.ToDescription();

		// Assert
		Assert.Equal(expectedDescription, actualDescription);
	}
}