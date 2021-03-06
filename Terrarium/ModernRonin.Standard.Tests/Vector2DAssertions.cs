﻿using FluentAssertions;

namespace ModernRonin.Standard.Tests
{
    public class Vector2DAssertions
    {
        readonly Vector2D mUnderTest;
        public Vector2DAssertions(Vector2D underTest) => mUnderTest = underTest;
        public AndConstraint<Vector2DAssertions> Approximate(Vector2D expected)
        {
            mUnderTest.X.OughtTo().BeApproximately(expected.X, 0.001f);
            mUnderTest.Y.OughtTo().BeApproximately(expected.Y, 0.001f);

            return new AndConstraint<Vector2DAssertions>(this);
        }
        public AndConstraint<Vector2DAssertions> Approximate(float expectedX, float expectedY)
        {
            mUnderTest.X.OughtTo().BeApproximately(expectedX, 0.001f);
            mUnderTest.Y.OughtTo().BeApproximately(expectedY, 0.001f);

            return new AndConstraint<Vector2DAssertions>(this);
        }
    }
}