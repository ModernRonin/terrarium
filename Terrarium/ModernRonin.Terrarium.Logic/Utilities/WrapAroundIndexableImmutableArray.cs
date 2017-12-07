using System.Collections;
using System.Collections.Generic;

namespace ModernRonin.Terrarium.Logic.Utilities
{
    public class WrapAroundIndexableImmutableArray<T> : IReadOnlyList<T>
    {
        readonly IReadOnlyList<T> mWrappee;
        public WrapAroundIndexableImmutableArray(IReadOnlyList<T> wrappee) => mWrappee = wrappee;
        public IEnumerator<T> GetEnumerator() => mWrappee.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public int Count => mWrappee.Count;
        public T this[int index] => mWrappee[index % Count];
    }
}