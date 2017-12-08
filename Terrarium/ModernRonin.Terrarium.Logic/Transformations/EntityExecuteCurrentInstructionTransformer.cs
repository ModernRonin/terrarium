using System;
using System.Collections.Generic;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Transformations.Framework;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public class EntityExecuteCurrentInstructionTransformer : AnEntityTransformer
    {
        readonly IInstructionExecutor mExecutor;
        public EntityExecuteCurrentInstructionTransformer(IInstructionExecutor executor) =>
            mExecutor = executor;
        public override IEnumerable<Type> Dependencies
        {
            get { yield return typeof(RemoveEntitiesWithNegativeTickEnergyTransformer); }
        }
        protected override IEntity Transform(IEntity old, ISimulationState state)
        {
            var currentInstruction = old.CurrentInstruction;
            return mExecutor.Execute(currentInstruction, old, state);
        }
    }
}