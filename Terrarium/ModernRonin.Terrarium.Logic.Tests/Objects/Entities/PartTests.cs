using FluentAssertions;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests.Objects.Entities
{
    [TestFixture]
    public class PartTests
    {
        [Test]
        public void BoundingBox_Has_Height_1()
        {
            new Part(PartKind.Absorber, new Vector2D(3, 4)).BoundingBox.Height.Should().Be(1);
        }
        [Test]
        public void BoundingBox_Has_Width_1()
        {
            new Part(PartKind.Absorber, new Vector2D(3, 4)).BoundingBox.Width.Should().Be(1);
        }
        [Test]
        public void BoundingBox_MinCorner_Equal_To_RelativePosition()
        {
            var underTest = new Part(PartKind.Absorber, new Vector2D(3, 4));
            underTest.BoundingBox.MinCorner.Should().Be(underTest.RelativePosition);
        }
        [Test]
        public void Code()
        {
            var underTest = new Part(PartKind.Absorber, new Vector2D(3.141f, 2.71f));
            underTest.Code.Should().Be("Absorber!3.141!2.71");
        }
    }
}