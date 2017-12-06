using System.Linq;
using FluentAssertions;
using ModernRonin.Standard;
using ModernRonin.Standard.Tests;
using ModernRonin.Terrarium.Logic.Objects;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using NSubstitute;
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
        [Test]
        public void Constructor_Sets_EnergyDensity_From_Density_If_Present()
        {
            var energySources = new[] { new EnergySource(Vector2D.Zero, 10f), new EnergySource(new Vector2D(2f, 0), 100f) };
            var underTest = new SimulationState(null, energySources, new Vector2D(5, 1), new float[2,2]{{17f, 17f}, {17f, 17f}});
            underTest.EnergyDensity[0, 0].OughtTo().Approximate(17f);
            underTest.EnergyDensity[0, 1].OughtTo().Approximate(17f);
            underTest.EnergyDensity[1, 1].OughtTo().Approximate(17f);
            underTest.EnergyDensity[1, 0].OughtTo().Approximate(17f);
        }
        [Test]
        public void Constructor_Sets_Size_If_Present()
        {
            new SimulationState(Null.Enumerable<Entity>(), Null.Enumerable<IEnergySource>(), new Vector2D(13, 17))
                .Size.OughtTo().Approximate(13, 17);
        }
        [Test]
        public void GetEntitiesAt_Returns_Entities_Whose_BoundingBox_Contains_Position()
        {
            var entities = new[]
            {
                Defaults.CrossPlant, Defaults.SnakePlant
            };
            var underTest = new SimulationState(
                entities,
                Null.Enumerable<IEnergySource>(),
                new Vector2D(100, 100)
                );
            underTest.GetEntitiesAt(new Vector2D(11, 10)).Single().Should().BeSameAs(entities[0]);
            underTest.GetEntitiesAt(new Vector2D(89, 90)).Single().Should().BeSameAs(entities[1]);
        }
        [Test]
        public void WithEnergySources_Returns_Different_Instance()
        {
            var underTest = new SimulationState(Null.Enumerable<Entity>(), Null.Enumerable<IEnergySource>());
            underTest.WithEnergySources(Null.Enumerable<IEnergySource>()).Should().NotBeSameAs(underTest);
        }
        [Test]
        public void WithEntities_Returns_Different_Instance()
        {
            var underTest = new SimulationState(Null.Enumerable<Entity>(), Null.Enumerable<IEnergySource>());
            underTest.WithEntities(Null.Enumerable<Entity>()).Should().NotBeSameAs(underTest);
        }
        [Test]
        public void WithEnergySources_Sets_EnergySources()
        {
            var underTest = new SimulationState(Null.Enumerable<Entity>(), Null.Enumerable<IEnergySource>());
            var energySources = new[] { Substitute.For<IEnergySource>(), Substitute.For<IEnergySource>() };
            underTest.WithEnergySources(energySources).EnergySources.Should().BeSameAs(energySources);
        }
        [Test]
        public void WithEntities_Sets_Entities()
        {
            var underTest = new SimulationState(Null.Enumerable<Entity>(), Null.Enumerable<IEnergySource>());
            var entities = new Entity[2];
            underTest.WithEntities(entities).Entities.Should().BeSameAs(entities);
        }
    }
}