using System.Linq;
using ModernRonin.Terrarium.Logic.Objects.Entities;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public class EntityEnergyAbsorptionTransformer : AnEntityTransformer
    {
        protected override Entity Transform(Entity old, ISimulationState state)
        {
            bool isAbsorber(Part part) => part.Kind == PartKind.Absorber;
            float energyFor(Part absorber) => state.EnergyDensityAt(old.State.Position + absorber.RelativePosition);

            var absorbers = old.State.Parts.Where(isAbsorber);
            var absorbed = absorbers.Select(energyFor).Sum();
            return old.WithState(old.State.AddTickEnergy(absorbed));
        }
    }
}