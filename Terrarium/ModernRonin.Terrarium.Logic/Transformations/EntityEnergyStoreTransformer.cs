using ModernRonin.Terrarium.Logic.Config;
using ModernRonin.Terrarium.Logic.Objects.Entities;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public class EntityEnergyStoreTransformer : AnEntityEnergyTransformer
    {
        readonly IPartPropertiesConfiguration mConfiguration;
        public EntityEnergyStoreTransformer(IPartPropertiesConfiguration configuration) =>
            mConfiguration = configuration;
        protected override Entity Transform(Entity old, ISimulationState state) => old;
    }
}