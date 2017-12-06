using System;
using System.Collections.Generic;
using ModernRonin.Terrarium.Logic.Objects.Entities;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public class EntityResetTickEnergyTransformer : AnEntityTransformer
    {
        public override IEnumerable<Type> Dependencies => NoTypes;
        protected override Entity Transform(Entity old, ISimulationState state) =>
            old.WithState(old.State.ResetTickEnergy());
    }
}