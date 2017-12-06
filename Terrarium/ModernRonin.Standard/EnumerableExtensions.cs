using System.Collections.Generic;

namespace ModernRonin.Standard
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> From<T>(T singleElement) => new[] {singleElement};
        public static IEnumerable<T> AsEnumerable<T>(this T singleElement) => From(singleElement);
    }
}