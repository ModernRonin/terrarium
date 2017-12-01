using FluentAssertions;
using NUnit.Framework;

namespace ModernRonin.Standard.Tests
{
    [TestFixture]
    public class Rectangle2DTests
    {
        [Test]
        public void Construction()
        {
            var underTest= new Rectangle2D(new Vector2D(1f, -3f), new Vector2D(17f, 2f));
            underTest.MaxCorner.OughtTo().Approximate(17f, 2f);
            underTest.MinCorner.OughtTo().Approximate(1f, -3f);
        }
        [Test]
        public void Width()
        {
            new Rectangle2D(new Vector2D(1f, -3f), new Vector2D(17f, 2f)).Width.OughtTo().Approximate(16f);
        }
        [Test]
        public void Height()
        {
            new Rectangle2D(new Vector2D(1f, -3f), new Vector2D(17f, 2f)).Height.OughtTo().Approximate(5f);
        }
        [Test]
        public void Diagonal()
        {
            new Rectangle2D(new Vector2D(1f, -3f), new Vector2D(17f, 2f)).Diagonal.OughtTo().Approximate(16f, 5f);
        }
    }
}