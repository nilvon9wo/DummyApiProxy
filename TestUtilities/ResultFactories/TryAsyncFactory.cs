using Ardalis.GuardClauses;

using LanguageExt;
using LanguageExt.ClassInstances.Pred;
using LanguageExt.Common;

using static LanguageExt.Prelude;

namespace FluxSzerviz.TestUtilities.ResultFactories;
public static class TryAsyncFactory
{
	public static TryAsync<T> CreateSuccess<T>(T value)
		=> new(async () => await Task.FromResult(Guard.Against.Null(value)));

	public static TryAsync<T> CreateFailure<T>(Exception ex, T? value = default)
		=> new(async () => await Task.Run<T>(() => ex != null
				? throw ex 
				: value!
		));
}
