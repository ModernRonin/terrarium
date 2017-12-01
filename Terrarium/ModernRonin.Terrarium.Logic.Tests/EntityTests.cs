using FluentAssertions;
using ModernRonin.Standard;
using ModernRonin.Standard.Tests;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests
{
    [TestFixture]
    public class EntityTests
    {
        [Test]
        public void BoundingBox()
        {
            Entity.Cross.BoundingBox.OughtTo().Approximate(-1, -1, 1, 1);
            Entity.Snake.BoundingBox.OughtTo().Approximate(-2, 0, 2, 0);
        }
        [Test]
        public void Code_Is_Different_If_Parts_Are()
        {
            Entity.Cross.Code.Should().NotBe(Entity.Snake.Code);
        }
        [Test]
        public void Code_Is_Equal_If_Parts_Are()
        {
            var cross1 = Entity.Cross.At(new Vector2D(2f, 3f));
            var cross2 = Entity.Cross.At(new Vector2D(4f, 5f));
            cross1.Code.Should().Be(cross2.Code);
        }
    }
}