using System;
using System.Collections.Generic;
using System.Linq;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Objects.Entities;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public class EntityPartsEnergyCostTransformer : AnEntityTransformer
    {
        readonly IEnergyCostConfiguration mConfiguration;
        public EntityPartsEnergyCostTransformer(IEnergyCostConfiguration configuration) => mConfiguration = configuration;
        public override IEnumerable<Type> Dependencies => typeof(EntityResetTickEnergyTransformer).AsEnumerable();
        protected override Entity Transform(Entity old, ISimulationState state)
        {
            float partCost(Part part) => mConfiguration.GetEnergyCostForPartKind(part.Kind);
            var cost = old.State.Parts.Select(partCost).Sum();
            return old.WithState(old.State.SubtractTickEnergy(cost));
        }
    }
}