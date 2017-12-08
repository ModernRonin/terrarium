using System;
using System.Collections.Generic;
using System.Linq;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Transformations.Execution;
using ModernRonin.Terrarium.Logic.Transformations.Framework;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public class EntityExecuteCurrentInstructionTransformer : ISimulationStateTransformerWithDependencies
    {
        readonly Dictionary<Type, IInstructionExecutor> mSpecificExecutors;
        public EntityExecuteCurrentInstructionTransformer(
            IEnumerable<IInstructionExecutor> specificExecutors) =>
            mSpecificExecutors = specificExecutors.ToDictionary(e => e.HandledInstructionType);
        public IEnumerable<Type> Dependencies
        {
            get { yield return typeof(RemoveEntitiesWithNegativeTickEnergyTransformer); }
        }
        public ISimulationState Transform(ISimulationState initialState)
        {
            ISimulationState transform(ISimulationState state, IEntity entity)
            {
                var currentInstruction = entity.CurrentInstruction;
                var type = currentInstruction.GetType();
                if (!mSpecificExecutors.ContainsKey(type)) throw new NotImplementedException();
                return mSpecificExecutors[type].Execute(currentInstruction, entity, state);
            }

            var snapshot = initialState.Entities.ToArray();
            return snapshot.Aggregate(initialState, transform);
        }
    }
}