using System.Linq;
using FluentAssertions;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Objects;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;
using ModernRonin.Terrarium.Logic.Transformations.Execution;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests.Transformations.Execution
{
    [TestFixture]
    public class ToggleThrustersExecutorTests
    {
        [Test]
        public void Handles_ToggleThrustersInstruction()
        {
            new ToggleThrustersExecutor().HandledInstructionType.Should().Be<ToggleThrustersInstruction>();
        }
        [Test]
        public void Increments_InstructionPointer()
        {
            var instruction = new ToggleThrustersInstruction();
            var entity = new Entity(new EntityState(Null.Enumerable<Part>()), new Genome(null, new[] {instruction}));
            var simulationState = new SimulationState(entity.AsEnumerable(), Null.Enumerable<IEnergySource>());

            var underTest = new ToggleThrustersExecutor();
            underTest.Execute(instruction, entity, simulationState).Entities.Single().State.CurrentInstructionIndex
                     .Should().Be(1);
        }
        [Test]
        public void Sets_Thrusters_To_Off_If_They_Were_On()
        {
            var instruction = new ToggleThrustersInstruction();
            var entity = new Entity(new EntityState(Null.Enumerable<Part>(), areThrustersOn: true),
                new Genome(null, new[] {instruction}));
            var simulationState = new SimulationState(entity.AsEnumerable(), Null.Enumerable<IEnergySource>());

            var underTest = new ToggleThrustersExecutor();
            underTest.Execute(instruction, entity, simulationState).Entities.Single().State.AreThrustersOn.Should()
                     .BeFalse();
        }
        [Test]
        public void Sets_Thrusters_To_On_If_They_Were_Off()
        {
            var instruction = new ToggleThrustersInstruction();
            var entity = new Entity(new EntityState(Null.Enumerable<Part>()), new Genome(null, new[] {instruction}));
            var simulationState = new SimulationState(entity.AsEnumerable(), Null.Enumerable<IEnergySource>());

            var underTest = new ToggleThrustersExecutor();
            underTest.Execute(instruction, entity, simulationState).Entities.Single().State.AreThrustersOn.Should()
                     .BeTrue();
        }
    }
}