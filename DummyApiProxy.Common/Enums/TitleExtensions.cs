using Ardalis.GuardClauses;

using System.ComponentModel;
using System.Reflection;

namespace DummyApiProxy.Common.Enums;

public static class TitleExtensions
{
	private static readonly Dictionary<string, Title> _titleEnumByDescriptionString 
		= Enum.GetValues(typeof(Title))
			  .Cast<Title>()
			  .Select(title => (Title: title, Description: GetDescription(title)))
			  .Where(pair => pair.Description != null)
			  .ToDictionary(
					pair => pair.Description, 
					pair => pair.Title, StringComparer.OrdinalIgnoreCase
				);

	public static string GetDescription(this Enum value)
	{
		FieldInfo? field = Guard.Against.Null(value)
			.GetType()
			.GetField(value.ToString());
		
		return field == null
			? ""
			: Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is not DescriptionAttribute attribute
				? value.ToString()
				: attribute.Description;
	}

	public static Title ToTitle(this string titleString) 
		=> _titleEnumByDescriptionString.TryGetValue(titleString, out Title title) 
			? title 
		: Title.None;

	public static string ToDescription(this Title title) 
		=> title.GetDescription();
}