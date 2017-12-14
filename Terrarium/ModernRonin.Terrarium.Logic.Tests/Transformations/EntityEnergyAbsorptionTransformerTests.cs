using System.Linq;
using ModernRonin.Standard;
using ModernRonin.Standard.Tests;
using ModernRonin.Terrarium.Logic.Objects;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Transformations;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests.Transformations
{
    [TestFixture]
    public class EntityEnergyAbsorptionTransformerTests
    {
        [Test]
        public void Adds_The_Sum_Of_Energy_At_All_AbsorberLocations()
        {
            var underTest = new EntityEnergyAbsorptionTransformer();
            var entity = new Entity(new EntityState(new[]
                    {
                        new Part(PartKind.Core, Vector2D.Zero), new Part(PartKind.Absorber, new Vector2D(-1, 0)),
                        new Part(PartKind.Absorber, new Vector2D(1, 0))
                    },
                    new Vector2D(10, 10)),
                null);
            var energyDensity = new float[100, 100];
            energyDensity[9, 10] = 13f;
            energyDensity[11, 10] = 17f;
            var state = new SimulationState(new[] {entity},
                Null.Enumerable<IEnergySource>(),
                new Vector2D(100, 100),
                energyDensity:energyDensity);

            var changed = underTest.Transform(state).Entities.Single();
            changed.State.TickEnergy.OughtTo().Approximate(30f);
        }
    }
}