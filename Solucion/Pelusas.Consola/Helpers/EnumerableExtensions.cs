
namespace Pelusas.Helpers;

internal static class EnumerableExtensions
{
	public static string JoinStrings (
		this IEnumerable<string> strings, string separator)
		=> string.Join(separator, strings);
}
