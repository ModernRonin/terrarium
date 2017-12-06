using FluentAssertions;
using ModernRonin.Standard;
using ModernRonin.Standard.Tests;
using ModernRonin.Terrarium.Logic.Objects;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests
{
    [TestFixture]
    public class SimulationStateTests
    {
        [Test]
        public void Constructor_Sets_Entities()
        {
            var entities = new Entity[] {new Entity(new EntityState(Null.Enumerable<Part>()), new Genome()),};
            new SimulationState(entities, Null.Enumerable<IEnergySource>()).Entities.Should().BeSameAs(entities);
        }
        [Test]
        public void Constructor_Sets_EnergySources()
        {
            var energySources = new[] {new EnergySource(Vector2D.Zero, 2f)};
            new SimulationState(null, energySources).EnergySources.Should().BeSameAs(energySources);
        }
        [Test]
        public void Constructor_Sets_EnergyDensity_From_EnergySources_If_No_Density_Passed()
        {
            var energySources = new[] { new EnergySource(Vector2D.Zero, 10f), new EnergySource(new Vector2D(2f, 0), 100f)   };
            var underTest = new SimulationState(null, energySources, new Vector2D(5, 1));
            underTest.EnergyDensity[0, 0].OughtTo().Approximate(108f);
            underTest.EnergyDensity[2, 0].OughtTo().Approximate(108f);
            underTest.EnergyDensity[4, 0].OughtTo().Approximate(104f);
        }
    }
}