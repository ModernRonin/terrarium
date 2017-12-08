using System.Linq;
using FluentAssertions;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Objects;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;
using ModernRonin.Terrarium.Logic.Transformations;
using NSubstitute;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests.Transformations
{
    [TestFixture]
    public class EntityExecuteCurrentInstructionTransformerTests
    {
        [Test]
        public void Increments_CurrentInstructionIndex()
        {
            var entity = new Entity(new EntityState(Null.Enumerable<Part>(), currentInstructionIndex:13), new Genome(null, new IInstruction[19]));
            var state = new SimulationState(entity.AsEnumerable(), Null.Enumerable<IEnergySource>());

            var underTest= new EntityExecuteCurrentInstructionTransformer();
            var changed = underTest.Transform(state).Entities.Single();

            changed.State.CurrentInstructionIndex.Should().Be(14);
        }
        [Test]
        public void Sets_CurrentInstructionIndex_To_Zero_If_It_Was_At_Maximum()
        {
            var entity = new Entity(new EntityState(Null.Enumerable<Part>(), currentInstructionIndex: 13), new Genome(null, new IInstruction[14]));
            var state = new SimulationState(entity.AsEnumerable(), Null.Enumerable<IEnergySource>());

            var underTest = new EntityExecuteCurrentInstructionTransformer();
            var changed = underTest.Transform(state).Entities.Single();

            changed.State.CurrentInstructionIndex.Should().Be(0);
        }
    }
}

