using System;
using System.Collections.Generic;

namespace ModernRonin.Standard
{
    public interface IDependent
    {
        IEnumerable<Type> Dependencies { get; }
    }

    public static class DependentExtensions
    {
        public static IEnumerable<T> SortByDependencies<T>(this IEnumerable<T> self) where T: IDependent
        {
            yield break;
        }
    }
}