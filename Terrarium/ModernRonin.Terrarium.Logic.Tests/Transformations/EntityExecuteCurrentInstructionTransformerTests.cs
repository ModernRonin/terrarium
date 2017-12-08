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
        public void Calls_Executor_With_CurrentInstruction()
        {
            var instruction = Substitute.For<IInstruction>();
            var entity = new Entity(Substitute.For<IEntityState>(), new Genome(new Parameters(), new[] {instruction}));
            var state = new SimulationState(entity.AsEnumerable(), Null.Enumerable<IEnergySource>());
            var executor = Substitute.For<IInstructionExecutor>();

            var underTest = new EntityExecuteCurrentInstructionTransformer(executor);
            underTest.Transform(state);

            executor.Received().Execute(instruction, entity, state);
        }
        [Test]
        public void Returns_Executors_ReturnValue()
        {
            var instruction = Substitute.For<IInstruction>();
            var entity = new Entity(Substitute.For<IEntityState>(), new Genome(new Parameters(), new[] {instruction}));
            var state = new SimulationState(entity.AsEnumerable(), Null.Enumerable<IEnergySource>());
            var executor = Substitute.For<IInstructionExecutor>();
            var newEntity = Substitute.For<IEntity>();
            executor.Execute(instruction, entity, state).Returns(newEntity);

            var underTest = new EntityExecuteCurrentInstructionTransformer(executor);
            underTest.Transform(state).Entities.Single().Should().BeSameAs(newEntity);
        }
    }
}