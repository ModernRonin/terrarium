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
}