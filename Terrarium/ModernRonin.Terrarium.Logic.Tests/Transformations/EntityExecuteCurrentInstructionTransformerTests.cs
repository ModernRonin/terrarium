using FluentAssertions;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Objects;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;
using ModernRonin.Terrarium.Logic.Transformations;
using ModernRonin.Terrarium.Logic.Transformations.Execution;
using NSubstitute;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests.Transformations
{
    [TestFixture]
    public class EntityExecuteCurrentInstructionTransformerTests
    {
        class AlphaInstruction : IInstruction { }

        class BravoInstruction : IInstruction { }

        [Test]
        public void Calls_Fitting_Executor_With_CurrentInstruction()
        {
            var instruction = new AlphaInstruction();
            var entity = new Entity(Substitute.For<IEntityState>(), new Genome(new Parameters(), new[] {instruction}));
            var state = new SimulationState(entity.AsEnumerable(), Null.Enumerable<IEnergySource>());
            var alphaExecutor = Substitute.For<IInstructionExecutor>();
            alphaExecutor.HandledInstructionType.Returns(typeof(AlphaInstruction));
            var bravoExecutor = Substitute.For<IInstructionExecutor>();
            bravoExecutor.HandledInstructionType.Returns(typeof(BravoInstruction));

            var underTest = new EntityExecuteCurrentInstructionTransformer(new[] {bravoExecutor, alphaExecutor});
            underTest.Transform(state);

            alphaExecutor.Received().Execute(instruction, entity, state);
            bravoExecutor.DidNotReceiveWithAnyArgs().Execute(null, null, null);
        }
        [Test]
        public void Returns_PickedExecutors_ReturnValue()
        {
            var instruction = new AlphaInstruction();
            var entity = new Entity(Substitute.For<IEntityState>(), new Genome(new Parameters(), new[] {instruction}));
            var state = new SimulationState(entity.AsEnumerable(), Null.Enumerable<IEnergySource>());
            var executor = Substitute.For<IInstructionExecutor>();
            executor.HandledInstructionType.Returns(typeof(AlphaInstruction));
            var newState = Substitute.For<ISimulationState>();
            executor.Execute(instruction, entity, state).Returns(newState);

            var underTest = new EntityExecuteCurrentInstructionTransformer(executor.AsEnumerable());
            underTest.Transform(state).Should().BeSameAs(newState);
        }
    }
}