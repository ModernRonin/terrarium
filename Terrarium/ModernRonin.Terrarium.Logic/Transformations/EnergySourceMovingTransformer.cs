using System;
using System.Collections.Generic;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Objects;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public class EnergySourceMovingTransformer : AnEnergySourceTransformer
    {
        protected override IEnergySource Transform(IEnergySource old, ISimulationState state)
        {
            var newPosition = (old.Position + old.Speed).ClampWithin(state.Size);
            return old.At(newPosition);
        }
        public override IEnumerable<Type> Dependencies => NoTypes;
    }
}