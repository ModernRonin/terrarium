using System;
using System.Collections.Generic;
using System.Linq;

namespace ModernRonin.Standard
{
    public static class DependentExtensions
    {
        public static IEnumerable<T> SortByDependencies<T>(this IEnumerable<T> self) where T : IDependent
        {
            var result = new List<T>();

            bool hasDependenciesFulfilled(T element) => element.Dependencies.All(d => result.Any(r => d == r.GetType()));

            var unresolved = new HashSet<T>(self);
            while (unresolved.Any())
            {
                var resolvable = unresolved.Where(hasDependenciesFulfilled).ToArray();
                if (!resolvable.Any()) throw new ArgumentException("The dependencies are circular.");
                result.AddRange(resolvable);
                unresolved.ExceptWith(resolvable);
            }
            return result;
        }
    }
}