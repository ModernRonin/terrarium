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
            scalarLeft.Should().Approximate(scalarRight);
        }
        [Test]
        public void OperatorBy_WithScalar_Multiplies()
        {
            (new Vector2D(1f, 2f) * 3f).Should().Approximate(3f, 6f);
        }
        [Test]
        public void OperatorDivide_Divides()
        {
            (new Vector2D(4f, 8f) / 2f).Should().Approximate(2f, 4f);
        }
        [Test]
        public void OperatorMinus_Subtracts()
        {
            var a = new Vector2D(1.1f, 2.2f);
            var b = new Vector2D(3.3f, 4.4f);
            (b - a).Should().Approximate(2.2f, 2.2f);
        }
        [Test]
        public void OperatorPlus_Adds()
        {
            var a = new Vector2D(1.1f, 2.2f);
            var b = new Vector2D(3.3f, 4.4f);
            (a + b).Should().Approximate(4.4f, 6.6f);
        }
    }
}