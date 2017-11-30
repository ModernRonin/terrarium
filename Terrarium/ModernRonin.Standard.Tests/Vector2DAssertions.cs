using FluentAssertions;

namespace ModernRonin.Standard.Tests
{
    public class Vector2DAssertions
    {
        readonly Vector2D mUnderTest;
        public Vector2DAssertions(Vector2D underTest) => mUnderTest = underTest;
        public AndConstraint<Vector2DAssertions> Approximate(Vector2D expected)
        {
            mUnderTest.X.Should().BeApproximately(expected.X, 0.001f);
            mUnderTest.Y.Should().BeApproximately(expected.Y, 0.001f);

            return new AndConstraint<Vector2DAssertions>(this);
        }
        public AndConstraint<Vector2DAssertions> Approximate(float expectedX, float expectedY)
        {
            mUnderTest.X.Should().BeApproximately(expectedX, 0.001f);
            mUnderTest.Y.Should().BeApproximately(expectedY, 0.001f);

            return new AndConstraint<Vector2DAssertions>(this);
        }
    }
}