using System.Collections;
using System.Collections.Generic;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;

namespace ModernRonin.Terrarium.Logic.Objects.Entities
{
    public class Genome
    {
        public Genome(Parameters parameters, IReadOnlyList<IInstruction> instructions)
        {
            Parameters = parameters;
            Instructions = new WrapAroundIndexableImmutableArray<IInstruction>(instructions);
        }
        public Parameters Parameters { get; }
        public IReadOnlyList<IInstruction> Instructions { get; }
    }

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