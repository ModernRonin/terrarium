using System.Collections.Generic;
using ModernRonin.Terrarium.Logic.Objects.Entities;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public abstract class AnEntityTransformer : AEnumeratingSimulationStateTransformer<Entity>
    {
        protected sealed override IEnumerable<Entity> Get(ISimulationState state) => state.Entities;
        protected sealed override ISimulationState Set(ISimulationState state, IEnumerable<Entity> property) =>
            state.WithEntities(property);
    }
}