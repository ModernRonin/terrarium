using System;
using System.Collections.Generic;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Objects.Entities;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public abstract class AnEntityEnergyTransformer : AnEntityTransformer
    {
        public sealed override IEnumerable<Type> Dependencies =>
            typeof(EntityResetTickEnergyTransformer).AsEnumerable();
        protected static Entity SubtractCost(Entity entity, float energy) => entity.WithState(entity.State.SubtractTickEnergy(energy));
        protected static Entity Add(Entity entity, float energy) => entity.WithState(entity.State.AddTickEnergy(energy));
    }

    public abstract class AnEntityEnergyCostTransformer : AnEntityEnergyTransformer
    {
        public AnEntityEnergyCostTransformer(IEnergyCostConfiguration configuration) => Configuration = configuration;
        public IEnergyCostConfiguration Configuration { get; }
        protected sealed override Entity Transform(Entity old, ISimulationState state)
        {
            var cost = CalculateCost(old);
            return SubtractCost(old, cost);
        }
        protected abstract float CalculateCost(Entity entity);
    }
}