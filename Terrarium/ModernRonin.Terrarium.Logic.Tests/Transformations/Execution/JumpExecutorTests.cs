using System.Linq;
using FluentAssertions;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Objects;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;
using ModernRonin.Terrarium.Logic.Transformations.Execution;
using NSubstitute;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests.Transformations.Execution
{
    [TestFixture]
    public class JumpExecutorTests
    {
        [Test]
        public void Handles_JumpInstruction()
        {
            new JumpExecutor().HandledInstructionType.Should().Be<JumpInstruction>();
        }
        [Test]
        public void Sets_CurrentInstructionIndex()
        {
            var instruction= new JumpInstruction(-2);
            var entity= new Entity(new EntityState(Null.Enumerable<Part>(), currentInstructionIndex:1), new Genome(null, new []
            {
                Substitute.For<IInstruction>(),
                Substitute.For<IInstruction>(),
                Substitute.For<IInstruction>(),
                Substitute.For<IInstruction>(),
            }));
            var simulationState= new SimulationState(entity.AsEnumerable(), Null.Enumerable<IEnergySource>());

            var underTest= new JumpExecutor();
            underTest.Execute(instruction, entity, simulationState).Entities.Single().State.CurrentInstructionIndex
                     .Should().Be(-1);
        }
    }
}

