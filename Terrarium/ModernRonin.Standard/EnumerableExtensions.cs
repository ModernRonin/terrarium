using System;
using System.Collections.Generic;
using System.Linq;

namespace ModernRonin.Standard
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> From<T>(T singleElement) => new[] {singleElement};
        public static IEnumerable<T> AsEnumerable<T>(this T singleElement) => From(singleElement);
        public static IDictionary<TEnum, TValue> EnumToDictionary<TEnum, TValue>(Func<TEnum, TValue> createValueForEnum)
        {
            if (!typeof(Enum).IsAssignableFrom(typeof(TEnum)))
                throw new ArgumentException($"The type parameter {nameof(TEnum)} must be an enum.");

            TEnum toKey(string name) => (TEnum) Enum.Parse(typeof(TEnum), name);

            TValue toValue(string name) => createValueForEnum(toKey(name));

            return Enum.GetNames(typeof(TEnum)).ToDictionary(toKey, toValue);
        }
        public static IEnumerable<T> Replace<T>(
            this IEnumerable<T> self,
            IEnumerable<T> toBeReplaced,
            Func<T, T> replacer) => Replace(self, toBeReplaced, replacer, EqualityComparer<T>.Default);
        public static IEnumerable<T> Replace<T>(
            this IEnumerable<T> self,
            IEnumerable<T> toBeReplaced,
            Func<T, T> replacer,
            IEqualityComparer<T> comparer)
        {
            var frozen = toBeReplaced as T[] ?? toBeReplaced.ToArray();

            T replace(T element) => frozen.Contains(element, comparer) ? replacer(element) : element;

            return self.Select(replace);
        }
    }
}