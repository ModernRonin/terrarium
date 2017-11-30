using FluentAssertions;
using NUnit.Framework;

namespace ModernRonin.Standard.Tests
{
    [TestFixture]
    public class Vector2DTests
    {
        [Test]
        public void Construction()
        {
            var underTest = new Vector2D(13.1f, 17.2f);
            underTest.X.Should().Be(13.1f);
            underTest.Y.Should().Be(17.2f);
        }
        [Test]
        public void OperatorBy_Gives_The_Same_Result_No_Matter_Which_Side_The_Scalar_Is_On()
        {
            var scalarLeft = 3f * new Vector2D(1f, 2f);
            var scalarRight = new Vector2D(1f, 2f) * 3f;
            scalarLeft.X.Should().BeApproximately(scalarRight.X, 0.001f);
            scalarLeft.Y.Should().BeApproximately(scalarRight.Y, 0.001f);
        }
        [Test]
        public void OperatorBy_WithScalar_Multiplies()
        {
            var result = new Vector2D(1f, 2f) * 3f;
            result.X.Should().BeApproximately(3f, 0.001f);
            result.Y.Should().BeApproximately(6f, 0.001f);
        }
        [Test]
        public void OperatorDivide_Divides()
        {
            var result = new Vector2D(4f, 8f) / 2f;
            result.X.Should().BeApproximately(2f, 0.001f);
            result.Y.Should().BeApproximately(4f, 0.001f);
        }
        [Test]
        public void OperatorMinus_Subtracts()
        {
            var a = new Vector2D(1.1f, 2.2f);
            var b = new Vector2D(3.3f, 4.4f);
            var result = b - a;
            result.X.Should().BeApproximately(2.2f, 0.001f);
            result.Y.Should().BeApproximately(2.2f, 0.001f);
        }
        [Test]
        public void OperatorPlus_Adds()
        {
            var a = new Vector2D(1.1f, 2.2f);
            var b = new Vector2D(3.3f, 4.4f);
            var result = a + b;
            result.X.Should().BeApproximately(4.4f, 0.001f);
            result.Y.Should().BeApproximately(6.6f, 0.001f);
        }
    }
}