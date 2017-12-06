using System.Linq;
using ModernRonin.Standard;
using ModernRonin.Standard.Tests;
using ModernRonin.Terrarium.Logic.Objects;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Transformations;
using NSubstitute;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests.Transformations
{
    [TestFixture]
    public class EntityPartsEnergyCostTransformerTests
    {
        [Test]
        public void Deducts_Cost_For_Parts()
        {
            var configuration = Substitute.For<IEnergyCostConfiguration>();
            configuration.GetEnergyCostForPartKind(PartKind.Absorber).Returns(0);
            configuration.GetEnergyCostForPartKind(PartKind.Core).Returns(3);
            configuration.GetEnergyCostForPartKind(PartKind.Store).Returns(7);
            var entity = new Entity(new EntityState(new[]
                {
                    new Part(PartKind.Absorber, Vector2D.Zero), new Part(PartKind.Core, Vector2D.Zero),
                    new Part(PartKind.Store, Vector2D.Zero), new Part(PartKind.Store, Vector2D.Zero)
                }),
                new Genome());
            var state = new SimulationState(entity.AsEnumerable(), Null.Enumerable<IEnergySource>());

            var underTest = new EntityPartsEnergyCostTransformer(configuration);
            var changed = underTest.Transform(state).Entities.Single();

            changed.State.TickEnergy.OughtTo().Approximate(-(1 * 0 + 1 * 3 + 2 * 7));
        }
    }
}