using System.Collections.Generic;

namespace ModernRonin.Terrarium.Logic.Transformations {
    public abstract class AnEntityTransformer : AEnumeratingSimulationStateTransformer<Entity>
    {
        protected override IEnumerable<Entity> Get(ISimulationState state) => state.Entities;
        protected override ISimulationState Set(ISimulationState state, IEnumerable<Entity> property) =>
            state.WithEntities(property);
    }
}