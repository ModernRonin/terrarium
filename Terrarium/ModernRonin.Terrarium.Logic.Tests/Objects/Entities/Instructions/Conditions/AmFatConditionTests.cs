﻿using FluentAssertions;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions.Conditions;
using NSubstitute;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests.Objects.Entities.Instructions.Conditions
{
    [TestFixture]
    public class AmFatConditionTests
    {
        [TestCase(10f, 20f, 5f, true)]
        [TestCase(10f, 20f, 30f, true)]
        [TestCase(10f, 20f, 31f, false)]
        [TestCase(20f, 10f, 5f, true)]
        [TestCase(20f, 10f, 30f, true)]
        [TestCase(20f, 10f, 31f, false)]
        public void Evaluates_Correctly(float tickEnergy, float storedEnergy, float threshold, bool expected)
        {
            var entity = Substitute.For<IEntity>();
            entity.State.TickEnergy.Returns(tickEnergy);
            entity.State.StoredEnergy.Returns(storedEnergy);
            entity.Genome.Parameters.Returns(new Parameters(fatThreshold: threshold));

            new AmFatCondition().IsFulfilledFor(entity).Should().Be(expected);
        }
    }
}