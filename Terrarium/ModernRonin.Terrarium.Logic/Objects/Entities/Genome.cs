using System.Collections.Generic;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;
using ModernRonin.Terrarium.Logic.Utilities;

namespace ModernRonin.Terrarium.Logic.Objects.Entities
{
    public class Genome : IGenome
    {
        public Genome(Parameters parameters, IReadOnlyList<IInstruction> instructions)
        {
            Parameters = parameters;
            Instructions = new WrapAroundIndexableImmutableArray<IInstruction>(instructions);
        }
        public Parameters Parameters { get; }
        public WrapAroundIndexableImmutableArray<IInstruction> Instructions { get; }
    }
}