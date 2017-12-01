using NUnit.Framework;

namespace ModernRonin.Standard.Tests
{
    [TestFixture]
    public class Rectangle2DTests
    {
        [Test]
        public void Construction()
        {
            var underTest = new Rectangle2D(new Vector2D(1f, -3f), new Vector2D(17f, 2f));
            underTest.MaxCorner.OughtTo().Approximate(17f, 2f);
            underTest.MinCorner.OughtTo().Approximate(1f, -3f);
        }
        [Test]
        public void Diagonal()
        {
            new Rectangle2D(new Vector2D(1f, -3f), new Vector2D(17f, 2f)).Diagonal.OughtTo().Approximate(16f, 5f);
        }
        [Test]
        public void Height()
        {
            new Rectangle2D(new Vector2D(1f, -3f), new Vector2D(17f, 2f)).Height.OughtTo().Approximate(5f);
        }
        [Test]
        public void Normalized_Has_MinCornerZeroZero()
        {
            var underTest = new Rectangle2D(new Vector2D(3, -5), new Vector2D(7, 2));
            underTest.Normalized.MinCorner.OughtTo().Approximate(0, 0);
        }
        [Test]
        public void Normalized_Has_Same_Height()
        {
            var underTest = new Rectangle2D(new Vector2D(3, -5), new Vector2D(7, 2));
            underTest.Normalized.Height.OughtTo().Approximate(underTest.Height);
        }
        [Test]
        public void Normalized_Has_Same_Width()
        {
            var underTest = new Rectangle2D(new Vector2D(3, -5), new Vector2D(7, 2));
            underTest.Normalized.Width.OughtTo().Approximate(underTest.Width);
        }
        [Test]
        public void Width()
        {
            new Rectangle2D(new Vector2D(1f, -3f), new Vector2D(17f, 2f)).Width.OughtTo().Approximate(16f);
        }
    }
}