using System.Linq;
using ModernRonin.Terrarium.Logic.Objects.Entities;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public class EntityEnergyAbsorptionTransformer : AnEntityTransformer
    {
        protected override Entity Transform(Entity old, ISimulationState state)
        {
            float absorbed = old.State.Parts.Where(p => p.Kind == PartKind.Absorber)
                                .Select(a => state.EnergyDensityAt(old.State.Position + a.RelativePosition)).Sum();
            return old.WithState(old.State.AddTickEnergy(absorbed));
        }
    }
}