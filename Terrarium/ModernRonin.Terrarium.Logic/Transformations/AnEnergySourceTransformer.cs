using System.Collections.Generic;
using ModernRonin.Terrarium.Logic.Objects;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public abstract class AnEnergySourceTransformer : AEnumeratingSimulationStateTransformer<IEnergySource>
    {
        protected override IEnumerable<IEnergySource> Get(ISimulationState state) => state.EnergySources;
        protected override ISimulationState Set(ISimulationState state, IEnumerable<IEnergySource> property) =>
            state.WithEnergySources(property);
    }
}