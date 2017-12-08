using System;
using System.Collections.Generic;
using System.Linq;
using ModernRonin.Terrarium.Logic.Transformations.Framework;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public class RemoveEntitiesWithNegativeTickEnergyTransformer : ISimulationStateTransformerWithDependencies
    {
        public IEnumerable<Type> Dependencies
        {
            get { yield return typeof(EntityEnergyStoreTransformer); }
        }
        public ISimulationState Transform(ISimulationState state)
        {
            var toSurvive = state.Entities.Where(e => e.State.TickEnergy >= 0);
            return state.WithEntities(toSurvive);
        }
    }
}