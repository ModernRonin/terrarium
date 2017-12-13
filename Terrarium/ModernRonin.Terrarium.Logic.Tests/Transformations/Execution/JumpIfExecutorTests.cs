using System.Linq;
using FluentAssertions;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Objects;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions.Conditions;
using ModernRonin.Terrarium.Logic.Transformations.Execution;
using NSubstitute;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests.Transformations.Execution
{
    [TestFixture]
    public class JumpIfExecutorTests
    {
        [Test]
        public void Handles_JumpIfInstruction()
        {
            new JumpIfExecutor().HandledInstructionType.Should().Be<JumpIfInstruction>();
        }
        [Test]
        public void Sets_CurrentInstructionIndex_PlusDelta_If_Condition_Evaluates_To_True()
        {
            var condition = Substitute.For<ICondition>();
            var instruction = new JumpIfInstruction(-2, condition);
            var entity = new Entity(new EntityState(Null.Enumerable<Part>(), currentInstructionIndex: 1),
                new Genome(null,
                    new[]
                    {
                        Substitute.For<IInstruction>(), Substitute.For<IInstruction>(), Substitute.For<IInstruction>(),
                        Substitute.For<IInstruction>()
                    }));
            var simulationState = new SimulationState(entity.AsEnumerable(), Null.Enumerable<IEnergySource>());
            condition.IsFulfilledFor(entity).Returns(true);

            var underTest = new JumpIfExecutor();
            underTest.Execute(instruction, entity, simulationState).Entities.Single().State.CurrentInstructionIndex
                     .Should().Be(-1);
        }
        [Test]
        public void Sets_Increments_CurrentInstructionIndex_If_Condition_Evaluates_To_False()
        {
            var condition = Substitute.For<ICondition>();
            var instruction = new JumpIfInstruction(-2, condition);
            var entity = new Entity(new EntityState(Null.Enumerable<Part>(), currentInstructionIndex: 1),
                new Genome(null,
                    new[]
                    {
                        Substitute.For<IInstruction>(), Substitute.For<IInstruction>(), Substitute.For<IInstruction>(),
                        Substitute.For<IInstruction>()
                    }));
            var simulationState = new SimulationState(entity.AsEnumerable(), Null.Enumerable<IEnergySource>());
            condition.IsFulfilledFor(entity).Returns(false);

            var underTest = new JumpIfExecutor();
            underTest.Execute(instruction, entity, simulationState).Entities.Single().State.CurrentInstructionIndex
                     .Should().Be(2);
        }
    }
}