using FluentAssertions;

namespace ModernRonin.Standard.Tests
{
    public class RectangleAssertions
    {
        readonly Rectangle2D mUnderTest;
        public RectangleAssertions(Rectangle2D underTest) => mUnderTest = underTest;
        public AndConstraint<RectangleAssertions> Approximate(float minX, float minY, float maxX, float maxY)
        {
            mUnderTest.MinCorner.OughtTo().Approximate(minX, minY);
            mUnderTest.MaxCorner.OughtTo().Approximate(maxX, maxY);

            return new AndConstraint<RectangleAssertions>(this);
        }
    }
}