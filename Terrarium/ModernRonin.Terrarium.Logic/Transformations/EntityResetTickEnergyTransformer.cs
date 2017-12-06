using ModernRonin.Terrarium.Logic.Objects.Entities;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public class EntityResetTickEnergyTransformer : AnEntityTransformer
    {
        public override int Priority => int.MinValue;
        protected override Entity Transform(Entity old, ISimulationState state) =>
            old.WithState(old.State.ResetTickEnergy());
    }
}