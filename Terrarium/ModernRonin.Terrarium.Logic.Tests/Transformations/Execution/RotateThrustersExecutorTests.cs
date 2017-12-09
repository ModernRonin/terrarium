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
    public class RotateThrustersExecutorTests
    {
        [Test]
        public void HandledInstructionType_Is_RotateThrustersInstruction()
        {
            new RotateThrustersExecutor().HandledInstructionType.Should().Be<RotateThrustersInstruction>();
        }
        [Test]
        public void Sets_ThrustDirection_From_Instruction()
        {
            var underTest = new RotateThrustersExecutor();
            var instruction = new RotateThrustersInstruction(new Vector2D(1, 2));
            var entity = new Entity(new EntityState(Null.Enumerable<Part>()), new Genome(null, new[] {instruction}));
            var state = new SimulationState(entity.AsEnumerable(), Null.Enumerable<IEnergySource>());
            underTest.Execute(instruction, entity, state).Entities.Single().State.ThrustDirection.Should()
                     .Be(instruction.NewRotation);
        }
    }
}