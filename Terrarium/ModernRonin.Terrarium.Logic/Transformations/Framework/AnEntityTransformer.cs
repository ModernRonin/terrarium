using System.Collections.Generic;
using ModernRonin.Terrarium.Logic.Objects.Entities;

namespace ModernRonin.Terrarium.Logic.Transformations.Framework
{
    public abstract class AnEntityTransformer : AnEnumeratingSimulationStateTransformer<IEntity>
    {
        protected sealed override IEnumerable<IEntity> Get(ISimulationState state) => state.Entities;
        protected sealed override ISimulationState Set(ISimulationState state, IEnumerable<IEntity> property) =>
            state.WithEntities(property);
    }
}