using System.Linq;
using FluentAssertions;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Objects;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Transformations;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests.Transformations
{
    [TestFixture]
    public class EntityResetTickEnergyTransformerTests
    {
        [Test]
        public void Sets_Entities_TickEnergy_To_Zero()
        {
            var underTest = new EntityResetTickEnergyTransformer();
            var entity = new Entity(new EntityState(Null.Enumerable<Part>(), tickEnergy: 14f), null);
            var state = new SimulationState(new[] {entity}, Null.Enumerable<IEnergySource>());

            var changed = underTest.Transform(state).Entities.Single();
            changed.State.TickEnergy.Should().Be(0);
        }
    }
}