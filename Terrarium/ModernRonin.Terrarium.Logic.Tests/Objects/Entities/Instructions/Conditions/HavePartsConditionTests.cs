using System.Collections.Generic;
using FluentAssertions;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using ModernRonin.Terrarium.Logic.Objects.Entities.Instructions.Conditions;
using NSubstitute;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests.Objects.Entities.Instructions.Conditions
{
    [TestFixture]
    public class HavePartsConditionTests
    {
        [TestCase(0, true)]
        [TestCase(2, false)]
        [TestCase(10, false)]
        public void Evaluates_Correctly_For_No(int actualCount, bool expected)
        {
            var entity = Substitute.For<IEntity>();
            entity.State.Parts.Returns(MakeParts(actualCount));

            var underTest = new HavePartsCondition(PartKind.Absorber, Multiplicity.No);

            underTest.IsFulfilledFor(entity).Should().Be(expected);
        }
        [TestCase(0, 0, false)]
        [TestCase(0, 1, false)]
        [TestCase(1, 1, true)]
        [TestCase(3, 5, true)]
        [TestCase(5, 5, true)]
        [TestCase(6, 5, false)]
        [TestCase(7, 5, false)]
        public void Evaluates_Correctly_For_Few(int actualCount, int threshold, bool expected)
        {
            var entity = Substitute.For<IEntity>();
            entity.State.Parts.Returns(MakeParts(actualCount));
            entity.Genome.Parameters.Returns(new Parameters(fewPartsThreshold: threshold));

            var underTest = new HavePartsCondition(PartKind.Absorber, Multiplicity.Few);

            underTest.IsFulfilledFor(entity).Should().Be(expected);
        }
        [TestCase(0, 0, false)]
        [TestCase(0, 1, false)]
        [TestCase(1, 1, true)]
        [TestCase(3, 5, false)]
        [TestCase(5, 5, true)]
        [TestCase(6, 5, true)]
        [TestCase(7, 5, true)]
        public void Evaluates_Correctly_For_Many(int actualCount, int threshold, bool expected)
        {
            var entity = Substitute.For<IEntity>();
            entity.State.Parts.Returns(MakeParts(actualCount));
            entity.Genome.Parameters.Returns(new Parameters(manyPartsThreshold: threshold));

            var underTest = new HavePartsCondition(PartKind.Absorber, Multiplicity.Many);

            underTest.IsFulfilledFor(entity).Should().Be(expected);
        }
        static IEnumerable<Part> MakeParts(int actualCount)
        {
            var parts = new Part[actualCount + 2];
            parts[0] = new Part(PartKind.Core, Vector2D.Zero);
            parts[1] = new Part(PartKind.Core, Vector2D.Zero);
            for (var i = 2; i < parts.Length; ++i) parts[i] = new Part(PartKind.Absorber, Vector2D.Zero);
            return parts;
        }
    }
}