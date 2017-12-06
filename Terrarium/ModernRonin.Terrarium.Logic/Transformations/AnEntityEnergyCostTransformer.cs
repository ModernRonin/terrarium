using System;
using System.Collections.Generic;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Objects.Entities;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public abstract class AnEntityEnergyCostTransformer : AnEntityTransformer
    {
        public AnEntityEnergyCostTransformer(IEnergyCostConfiguration configuration) => Configuration = configuration;
        public IEnergyCostConfiguration Configuration { get; }
        public sealed override IEnumerable<Type> Dependencies =>
            typeof(EntityResetTickEnergyTransformer).AsEnumerable();
        protected sealed override Entity Transform(Entity old, ISimulationState state)
        {
            var cost = CalculateCost(old);
            return old.WithState(old.State.SubtractTickEnergy(cost));
        }
        protected abstract float CalculateCost(Entity entity);
    }
}