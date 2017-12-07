using System.Linq;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Transformations.Framework;
using ModernRonin.Terrarium.Logic.Utilities;

namespace ModernRonin.Terrarium.Logic.Transformations
{
    public class EntityEnergyAbsorptionTransformer : AnEntityEnergyTransformer
    {
        protected override Entity Transform(Entity old, ISimulationState state)
        {
            float energyFor(Part absorber) => state.EnergyDensityAt(old.State.Position + absorber.RelativePosition);

            var absorbers = old.State.Parts.OfKind(PartKind.Absorber);
            var absorbed = absorbers.Select(energyFor).Sum();

            return Add(old, absorbed);
        }
    }
}