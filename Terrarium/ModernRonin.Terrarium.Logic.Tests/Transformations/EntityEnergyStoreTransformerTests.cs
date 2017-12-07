using System;
using System.Linq;
using ModernRonin.Standard;
using ModernRonin.Standard.Tests;
using ModernRonin.Terrarium.Logic.Config;
using ModernRonin.Terrarium.Logic.Objects;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Transformations;
using NSubstitute;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests.Transformations
{
    [TestFixture]
    public class EntityEnergyStoreTransformerTests
    {
        [TestCase(100, 0, 86, 14)]
        [TestCase(100, 0, 86, 14)]
        [TestCase(5, 14, 5, 14)]
        [TestCase(5, 4, 0, 9)]
        [TestCase(100, 4, 90, 14)]
        [TestCase(0, 11, 0, 11)]
        [TestCase(-1, 0, -1, 0)]
        [TestCase(-1, 3, 0, 2)]
        [TestCase(-7, 3, -4, 0)]
        public void TickEnergy_And_StoredEnergy_Are_Updated_Correctly(int initialTickEnergy, int initialStoredEnergy, int expectedTickEnergy, int expectedStoredEnergy)
        {
            if (initialTickEnergy + initialStoredEnergy != expectedTickEnergy + expectedStoredEnergy)
                throw new ArgumentException("Sums don't add up");
            var config = Substitute.For<IPartPropertiesConfiguration>();
            config.CapacityOfStores.Returns(7);
            var underTest = new EntityEnergyStoreTransformer(config);

            var entity =
                new Entity(new EntityState(new[]
                            {new Part(PartKind.Store, Vector2D.Zero), new Part(PartKind.Store, Vector2D.Zero)},
                        tickEnergy: initialTickEnergy,
                        storedEnergy: initialStoredEnergy),
                    null);
            var state = new SimulationState(entity.AsEnumerable(), Null.Enumerable<IEnergySource>());

            var changed = underTest.Transform(state).Entities.Single();
            changed.State.TickEnergy.OughtTo().Approximate(expectedTickEnergy);
            changed.State.StoredEnergy.OughtTo().Approximate(expectedStoredEnergy);
        }
    }
}