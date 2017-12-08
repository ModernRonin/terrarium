using System;
using System.Collections.Generic;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;
using ModernRonin.Terrarium.Logic.Transformations.Framework;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public class EntityExecuteCurrentEntityChangingInstructionTransformer : AnEntityTransformer
    {
        readonly IEntityChangingInstructionExecutor mExecutor;
        public EntityExecuteCurrentEntityChangingInstructionTransformer(IEntityChangingInstructionExecutor executor) =>
            mExecutor = executor;
        public override IEnumerable<Type> Dependencies
        {
            get { yield return typeof(RemoveEntitiesWithNegativeTickEnergyTransformer); }
        }
        protected override IEntity Transform(IEntity old, ISimulationState state)
        {
            if (old.CurrentInstruction is IEntityChangingInstruction currentInstruction)
                return mExecutor.Execute(currentInstruction, old, state);
            return old;
        }
    }
}