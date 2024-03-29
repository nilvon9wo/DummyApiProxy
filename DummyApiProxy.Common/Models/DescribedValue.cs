namespace DummyApiProxy.Common.Models;

internal record struct DescribedValue<T>(T Value, string Description)
{
	public static implicit operator (T Value, string Description)(DescribedValue<T> value)
		=> (value.Value, value.Description);

	public static implicit operator DescribedValue<T>((T Value, string Description) value)
		=> new(value.Value, value.Description);
}