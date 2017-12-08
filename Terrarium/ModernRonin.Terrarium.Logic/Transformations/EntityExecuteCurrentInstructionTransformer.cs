using System;
using System.Collections.Generic;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Transformations.Framework;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public class EntityExecuteCurrentInstructionTransformer : AnEntityTransformer
    {
        public override IEnumerable<Type> Dependencies
        {
            get { yield return typeof(RemoveEntitiesWithNegativeTickEnergyTransformer); }
        }
        protected override IEntity Transform(IEntity old, ISimulationState state)
        {
            var currentInstruction = old.CurrentInstruction;

            var nextInstructionIndex = old.State.CurrentInstructionIndex + 1;
            if (nextInstructionIndex >= old.Genome.Instructions.Count) nextInstructionIndex = 0;

            return old.WithState(old.State.WithCurrentInstructionIndex(nextInstructionIndex));
        }
    }
}