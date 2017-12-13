using FluentAssertions;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions.Conditions;
using NSubstitute;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests.Objects.Entities.Instructions.Conditions
{
    [TestFixture]
    public class AmMovingConditionTests
    {
        [TestCase(0.005f, false)]
        [TestCase(0.01f, false)]
        [TestCase(0.1f, false)]
        [TestCase(0.11f, true)]
        [TestCase(0.5f, true)]
        [TestCase(1f, true)]
        public void Evaluates_Correctly(float lastSquaredDistance, bool expected)
        {
            var entity = Substitute.For<IEntity>();
            entity.State.LastDistanceMovedSquared.Returns(lastSquaredDistance);

            new AmMovingCondition().IsFulfilledFor(entity).Should().Be(expected);
        }
    }
}