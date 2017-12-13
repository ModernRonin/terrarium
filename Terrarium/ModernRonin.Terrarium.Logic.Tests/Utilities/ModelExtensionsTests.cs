using System.Collections.Generic;
using FluentAssertions;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions;
using ModernRonin.Terrarium.Logic.Utilities;
using NSubstitute;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests.Utilities
{
    [TestFixture]
    public class ModelExtensionsTests
    {
        [Test]
        public void ReplaceEntity_Returns_State_With_Changed_Entities()
        {
            var alpha = Substitute.For<IEntity>();
            var bravo = Substitute.For<IEntity>();
            var entities = new[]
                {Substitute.For<IEntity>(), Substitute.For<IEntity>(), alpha, Substitute.For<IEntity>()};
            var state = Substitute.For<ISimulationState>();
            state.Entities.Returns(entities);
            var newState = Substitute.For<ISimulationState>();
            IEnumerable<IEntity> newEntities = null;
            state.WithEntities(Arg.Do<IEnumerable<IEntity>>(e => newEntities = e)).Returns(newState);

            state.ReplaceEntity(alpha, bravo).Should().BeSameAs(newState);
            newEntities.Should().HaveCount(4);
            newEntities.Should().Contain(bravo);
            newEntities.Should().Contain(entities[0]);
            newEntities.Should().Contain(entities[1]);
            newEntities.Should().Contain(entities[3]);
        }
        [Test]
        public void WithJump_Adjust_CurrentInstructionIndex()
        {
            var state = new EntityState(Null.Enumerable<Part>(), currentInstructionIndex: 7);
            var jmp = new JumpInstruction(-3);

            state.WithJump(jmp).CurrentInstructionIndex.Should().Be(4);
        }
    }
}