using FluentAssertions;
using ModernRonin.Standard;
using ModernRonin.Standard.Tests;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests
{
    [TestFixture]
    public class EnergySourceTests
    {
        [Test]
        public void At_Returns_New_Instance_With_Passed_Position()
        {
            var result = new EnergySource(new Vector2D(), 13f).At(new Vector2D(1f, 1f));

            result.Position.Should().Approximate(new Vector2D(1f, 1f));
        }
        [Test]
        public void Construction()
        {
            var underTest = new EnergySource(new Vector2D(2.3f, 3.1f), 17f);
            underTest.Position.X.Should().BeApproximately(2.3f, 0.001f);
            underTest.Position.Y.Should().BeApproximately(3.1f, 0.001f);
            underTest.Intensity.Should().BeApproximately(17f, 0.001f);
        }
    }
}