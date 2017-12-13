using FluentAssertions;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions.Conditions;
using NSubstitute;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests.Objects.Entities.Instructions.Conditions
{
    [TestFixture]
    public class AmMovingQuicklyConditionTests
    {
        [TestCase(0.3f, 0.2f, true)]
        [TestCase(0.25f, 0.2f, true)]
        [TestCase(0.25f, 0.25f, true)]
        [TestCase(0.25f, 0.3f, false)]
        public void Evaluates_Correctly(float lastSquaredDistance, float threshold, bool expected)
        {
            var entity = Substitute.For<IEntity>();
            entity.State.LastDistanceMovedSquared.Returns(lastSquaredDistance);
            entity.Genome.Parameters.Returns(new Parameters(movingQuicklyThreshold: threshold));

            new AmMovingQuicklyCondition().IsFulfilledFor(entity).Should().Be(expected);
        }
    }
}