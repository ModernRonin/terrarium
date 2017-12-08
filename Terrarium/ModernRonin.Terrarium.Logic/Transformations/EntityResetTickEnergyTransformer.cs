using System;
using System.Collections.Generic;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Transformations.Framework;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public class EntityResetTickEnergyTransformer : AnEntityTransformer
    {
        public override IEnumerable<Type> Dependencies => NoTypes;
        protected override IEntity Transform(IEntity old, ISimulationState state) =>
            old.WithState(old.State.ResetTickEnergy());
    }
}