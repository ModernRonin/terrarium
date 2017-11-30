using FluentAssertions;
using ModernRonin.Standard;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests
{
    [TestFixture]
    public class EnergySourceTests
    {
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