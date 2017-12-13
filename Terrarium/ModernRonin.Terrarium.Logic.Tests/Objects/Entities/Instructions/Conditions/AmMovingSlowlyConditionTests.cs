using FluentAssertions;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions.Conditions;
using NSubstitute;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests.Objects.Entities.Instructions.Conditions {
    [TestFixture]
    public class AmMovingSlowlyConditionTests
    {
        [TestCase(0.3f, 0.2f, false)]
        [TestCase(0.25f, 0.2f, false)]
        [TestCase(0.25f, 0.25f, true)]
        [TestCase(0.25f, 0.3f, true)]
        public void Evaluates_Correctly(float lastSquaredDistance, float threshold, bool expected)
        {
            var entity = Substitute.For<IEntity>();
            entity.State.LastDistanceMovedSquared.Returns(lastSquaredDistance);
            entity.Genome.Parameters.Returns(new Parameters(movingSlowlyThreshold: threshold));

            new AmMovingSlowlyCondition().IsFulfilledFor(entity).Should().Be(expected);
        }
    }
}