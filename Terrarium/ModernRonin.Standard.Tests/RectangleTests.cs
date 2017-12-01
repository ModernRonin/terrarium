using FluentAssertions;
using NUnit.Framework;

namespace ModernRonin.Standard.Tests
{
    [TestFixture]
    public class RectangleTests
    {
        [Test]
        public void Construction()
        {
            var underTest= new Rectangle(new Vector2D(1f, -3f), new Vector2D(17f, 2f));
            underTest.MaxCorner.Should().Approximate(17f, 2f);
            underTest.MinCorner.Should().Approximate(1f, -3f);
        }
        [Test]
        public void Width()
        {
            new Rectangle(new Vector2D(1f, -3f), new Vector2D(17f, 2f)).Width.Should().Approximate(16f);
        }
        [Test]
        public void Height()
        {
            new Rectangle(new Vector2D(1f, -3f), new Vector2D(17f, 2f)).Height.Should().Approximate(5f);
        }
        [Test]
        public void Diagonal()
        {
            new Rectangle(new Vector2D(1f, -3f), new Vector2D(17f, 2f)).Diagonal.Should().Approximate(16f, 5f);
        }
    }
}