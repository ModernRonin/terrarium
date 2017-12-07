using System.Linq;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Objects;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Transformations;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests.Transformations
{
    [TestFixture]
    public class EntityEnergyStoreTransformerTests
    {
        [Test]
        public void Decreases_TickEnergy_By_As_Much_As_Possible_From_Stores()
        {
            var entity = new Entity(new EntityState(
                new []
                {
                    new Part(PartKind.Store, Vector2D.Zero), 
                    new Part(PartKind.Store, Vector2D.Zero)
                },tickEnergy: 100), null);
            var state= new SimulationState(entity.AsEnumerable(), Null.Enumerable<IEnergySource>());
            var underTest = new EntityEnergyStoreTransformer();

            var changed = underTest.Transform(state).Entities.Single();


        }
    }
}

