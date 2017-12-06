using System;
using System.Collections.Generic;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Objects.Entities;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public class EntityEnergyCostTransformer : AnEntityTransformer
    {
        readonly IEnergyCostConfiguration mConfiguration;
        public EntityEnergyCostTransformer(IEnergyCostConfiguration configuration) => mConfiguration = configuration;
        public override IEnumerable<Type> Dependencies => typeof(EntityResetTickEnergyTransformer).AsEnumerable();
        protected override Entity Transform(Entity old, ISimulationState state) => old;
    }
}