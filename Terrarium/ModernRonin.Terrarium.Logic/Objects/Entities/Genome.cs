using System.Collections.Generic;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;

namespace ModernRonin.Terrarium.Logic.Objects.Entities
{
    public class Genome
    {
        public Parameters Parameters { get; }
        public IList<IInstruction> Instructions { get; }
    }
}