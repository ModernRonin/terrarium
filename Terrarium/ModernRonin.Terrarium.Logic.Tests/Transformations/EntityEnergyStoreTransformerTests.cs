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
        [Test]
        public void Adds_Full_TickEnergy_To_StoredEnergy_If_Enough_StorageCapacity()
        {
            var config = Substitute.For<IPartPropertiesConfiguration>();
            config.CapacityOfStores.Returns(7);
            var underTest = new EntityEnergyStoreTransformer(config);

            var entity =
                new Entity(new EntityState(new[]
                            {new Part(PartKind.Store, Vector2D.Zero), new Part(PartKind.Store, Vector2D.Zero)},
                        tickEnergy: 5,
                        storedEnergy: 4),
                    null);
            var state = new SimulationState(entity.AsEnumerable(), Null.Enumerable<IEnergySource>());

            var changed = underTest.Transform(state).Entities.Single();
            changed.State.StoredEnergy.OughtTo().Approximate(4 + 5);
        }
        [Test]
        public void Decreases_TickEnergy_By_FullStoreCapacity_If_Stores_Are_Empty()
        {
            var config = Substitute.For<IPartPropertiesConfiguration>();
            config.CapacityOfStores.Returns(7);
            var underTest = new EntityEnergyStoreTransformer(config);

            var entity = new Entity(new EntityState(new[]
                        {new Part(PartKind.Store, Vector2D.Zero), new Part(PartKind.Store, Vector2D.Zero)},
                    tickEnergy: 100),
                null);
            var state = new SimulationState(entity.AsEnumerable(), Null.Enumerable<IEnergySource>());

            var changed = underTest.Transform(state).Entities.Single();
            changed.State.TickEnergy.OughtTo().Approximate(100 - 14);
        }
        [Test]
        public void Decreases_TickEnergy_By_RemainingStoreCapacity_If_Stores_Are_PartiallyFull()
        {
            var config = Substitute.For<IPartPropertiesConfiguration>();
            config.CapacityOfStores.Returns(7);
            var underTest = new EntityEnergyStoreTransformer(config);

            var entity =
                new Entity(new EntityState(new[]
                            {new Part(PartKind.Store, Vector2D.Zero), new Part(PartKind.Store, Vector2D.Zero)},
                        tickEnergy: 100,
                        storedEnergy: 4),
                    null);
            var state = new SimulationState(entity.AsEnumerable(), Null.Enumerable<IEnergySource>());

            var changed = underTest.Transform(state).Entities.Single();
            changed.State.TickEnergy.OughtTo().Approximate(100 - (14 - 4));
        }
        [Test]
        public void DoesNot_Change_StoredEnergy_If_Stores_Are_Full()
        {
            var config = Substitute.For<IPartPropertiesConfiguration>();
            config.CapacityOfStores.Returns(7);
            var underTest = new EntityEnergyStoreTransformer(config);

            var entity =
                new Entity(new EntityState(new[]
                            {new Part(PartKind.Store, Vector2D.Zero), new Part(PartKind.Store, Vector2D.Zero)},
                        tickEnergy: 5,
                        storedEnergy: 14),
                    null);
            var state = new SimulationState(entity.AsEnumerable(), Null.Enumerable<IEnergySource>());

            var changed = underTest.Transform(state).Entities.Single();
            changed.State.StoredEnergy.OughtTo().Approximate(14);
        }
        [Test]
        public void DoesNot_Change_TickEnergy_If_Stores_Are_Full()
        {
            var config = Substitute.For<IPartPropertiesConfiguration>();
            config.CapacityOfStores.Returns(7);
            var underTest = new EntityEnergyStoreTransformer(config);

            var entity =
                new Entity(new EntityState(new[]
                            {new Part(PartKind.Store, Vector2D.Zero), new Part(PartKind.Store, Vector2D.Zero)},
                        tickEnergy: 5,
                        storedEnergy: 14),
                    null);
            var state = new SimulationState(entity.AsEnumerable(), Null.Enumerable<IEnergySource>());

            var changed = underTest.Transform(state).Entities.Single();
            changed.State.TickEnergy.OughtTo().Approximate(5);
        }
        [Test]
        public void DoesNotDecrease_TickEnergy_To_Less_Than_Zero_If_StorageCapacity_Bigger_Than_TickEnergy()
        {
            var config = Substitute.For<IPartPropertiesConfiguration>();
            config.CapacityOfStores.Returns(7);
            var underTest = new EntityEnergyStoreTransformer(config);

            var entity =
                new Entity(new EntityState(new[]
                            {new Part(PartKind.Store, Vector2D.Zero), new Part(PartKind.Store, Vector2D.Zero)},
                        tickEnergy: 5,
                        storedEnergy: 4),
                    null);
            var state = new SimulationState(entity.AsEnumerable(), Null.Enumerable<IEnergySource>());

            var changed = underTest.Transform(state).Entities.Single();
            changed.State.TickEnergy.OughtTo().Approximate(5 - 5);
        }
        [Test]
        public void FillsUp_StoredEnergy_To_FullStoredCapacity_If_Enough_TickEnergy()
        {
            var config = Substitute.For<IPartPropertiesConfiguration>();
            config.CapacityOfStores.Returns(7);
            var underTest = new EntityEnergyStoreTransformer(config);

            var entity =
                new Entity(new EntityState(new[]
                            {new Part(PartKind.Store, Vector2D.Zero), new Part(PartKind.Store, Vector2D.Zero)},
                        tickEnergy: 100,
                        storedEnergy: 4),
                    null);
            var state = new SimulationState(entity.AsEnumerable(), Null.Enumerable<IEnergySource>());

            var changed = underTest.Transform(state).Entities.Single();
            changed.State.StoredEnergy.OughtTo().Approximate(14);
        }
    }
}