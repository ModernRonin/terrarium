using System.Collections.Generic;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;

namespace ModernRonin.Terrarium.Logic.Objects.Entities
{
    public class Genome
    {
        public Genome(Parameters parameters, IList<IInstruction> instructions)
        {
            Parameters = parameters;
            Instructions = instructions;
        }
        public Parameters Parameters { get; }
        public IList<IInstruction> Instructions { get; }
    }
}