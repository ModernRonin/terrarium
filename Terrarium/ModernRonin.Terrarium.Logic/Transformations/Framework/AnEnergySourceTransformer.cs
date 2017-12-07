using System.Collections.Generic;
using ModernRonin.Terrarium.Logic.Objects;

namespace ModernRonin.Terrarium.Logic.Transformations.Framework
{
    public abstract class AnEnergySourceTransformer : AEnumeratingSimulationStateTransformer<IEnergySource>
    {
        protected sealed override IEnumerable<IEnergySource> Get(ISimulationState state) => state.EnergySources;
        protected sealed override ISimulationState Set(ISimulationState state, IEnumerable<IEnergySource> property) =>
            state.WithEnergySources(property);
    }
}