using System;
using System.Collections.Generic;
using System.Linq;
using ModernRonin.Terrarium.Logic.Config;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Transformations.Framework;
using ModernRonin.Terrarium.Logic.Utilities;

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
        protected override IEntity Transform(IEntity old, ISimulationState state)
        {
            if (old.State.TickEnergy > 0)
            {
                var totalStorageCapacity =
                    old.State.Parts.OfKind(PartKind.Store).Count() * mConfiguration.CapacityOfStores;
                var remainingStorageCapacity = totalStorageCapacity - old.State.StoredEnergy;
                var toBeStored = Math.Min(old.State.TickEnergy, remainingStorageCapacity);
                return old.WithState(old.State.AddStoredEnergy(toBeStored).SubtractTickEnergy(toBeStored));
            }
            if (old.State.TickEnergy < 0)
            {
                var needed = -old.State.TickEnergy;
                var available = old.State.StoredEnergy;
                var toBeUsed = Math.Min(needed, available);
                return old.WithState(old.State.SubtractStoredEnergy(toBeUsed).AddTickEnergy(toBeUsed));
            }
            return old;
        }
    }
}