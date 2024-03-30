using Ardalis.GuardClauses;

using FluxSzerviz.DummyApiProxy.Common.Models;

using System.ComponentModel;
using System.Reflection;

namespace FluxSzerviz.DummyApiProxy.Common.Titles;

public static class TitleExtensions
{
	private static readonly Dictionary<string, Title> _titleEnumByDescriptionString
		= Enum.GetValues(typeof(Title))
			  .Cast<Title>()
			  .Select(title => new DescribedValue<Title>(title, title.GetDescription()))
			  .Where(pair => pair.Description != null)
			  .ToDictionary(
					pair => pair.Description,
					pair => pair.Value, StringComparer.OrdinalIgnoreCase
				);

	public static Title ToTitle(this string titleString)
		=> string.IsNullOrWhiteSpace(titleString)
			? Title.None
			: _titleEnumByDescriptionString.TryGetValue(titleString.Trim(), out Title title)
				? title
				: Title.None;

	public static string ToDescription(this Title title)
		=> title.GetDescription();

	private static string GetDescription(this Enum value)
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
}
