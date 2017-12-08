using System.Collections.Generic;
using ModernRonin.Terrarium.Logic.Objects.Entities;

namespace ModernRonin.Terrarium.Logic.Transformations.Framework
{
    public abstract class AnEntityTransformer : AnEnumeratingSimulationStateTransformer<Entity>
    {
        protected sealed override IEnumerable<Entity> Get(ISimulationState state) => state.Entities;
        protected sealed override ISimulationState Set(ISimulationState state, IEnumerable<Entity> property) =>
            state.WithEntities(property);
    }
}