using System;
using System.Collections.Generic;
using System.Linq;
using ModernRonin.Terrarium.Logic.Config;
using ModernRonin.Terrarium.Logic.Objects.Entities;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public class EntityEnergyStoreTransformer : AnEntityEnergyTransformer
    {
        readonly IPartPropertiesConfiguration mConfiguration;
        public EntityEnergyStoreTransformer(IPartPropertiesConfiguration configuration) =>
            mConfiguration = configuration;
        protected override IEnumerable<Type> AdditionalDependencies
        {
            get
            {
                yield return typeof(EntityEnergyAbsorptionTransformer);
                yield return typeof(EntityCurrentInstructionCostTransformer);
                yield return typeof(EntityPartsEnergyCostTransformer);
                
            }
        }
        protected override Entity Transform(Entity old, ISimulationState state)
        {
            if (old.State.TickEnergy > 0)
            {
                var remainingStorageCapacity = old.State.Parts.Where(p => p.Kind==PartKind.Absorber);
                var toBeStored = 0;
                return old.WithState(old.State.AddStoredEnergy(toBeStored));
            }
            return old;
        }
    }
}