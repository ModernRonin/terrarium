using ModernRonin.Terrarium.Logic.Objects.Entities;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public class EntityEnergyStoreTransformer : AnEntityEnergyTransformer
    {
        protected override Entity Transform(Entity old, ISimulationState state) => old;
    }
}