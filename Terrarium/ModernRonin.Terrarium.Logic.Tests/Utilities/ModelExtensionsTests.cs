using System.Collections.Generic;
using System.Linq;
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
        public void InsertPartPushing_Adds_Part_At_Tail()
        {
            var parts = new[]
            {
                new Part(PartKind.Core, Vector2D.Zero), new Part(PartKind.Absorber, Vector2D.One),
                new Part(PartKind.Store, Vector2D.Two)
            };
            var insertee = new Part(PartKind.Thruster, Vector2D.One);
            var direction = Vector2D.One;
            var targetPosition = Vector2D.Three;

            parts.InsertPartPushing(insertee, direction, targetPosition).Last().Should().BeSameAs(insertee);
        }
        [Test]
        public void InsertPartPushing_Pushes_Parts_Towards_TargetPosition()
        {
            var parts = new[]
            {
                new Part(PartKind.Core, Vector2D.Zero), new Part(PartKind.Absorber, Vector2D.One),
                new Part(PartKind.Store, Vector2D.Two)
            };
            var insertee = new Part(PartKind.Thruster, Vector2D.One);
            var direction = Vector2D.One;
            var targetPosition = Vector2D.Three;

            var output = parts.InsertPartPushing(insertee, direction, targetPosition).ToArray();

            output.Should().HaveCount(4);
            output[0].Should().BeSameAs(parts[0]);
            output[1].RelativePosition.Should().Be(Vector2D.Two);
            output[2].RelativePosition.Should().Be(Vector2D.Three);
            output[3].RelativePosition.Should().Be(Vector2D.One);
            output[1].Kind.Should().Be(PartKind.Absorber);
            output[2].Kind.Should().Be(PartKind.Store);
            output[3].Kind.Should().Be(PartKind.Thruster);
        }
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