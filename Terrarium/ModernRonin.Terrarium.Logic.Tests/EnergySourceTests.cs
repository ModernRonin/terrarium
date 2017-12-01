using ModernRonin.Standard;
using ModernRonin.Standard.Tests;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests
{
    [TestFixture]
    public class EnergySourceTests
    {
        [Test]
        public void ApplyTo_Adds_Full_Intensity_At_Position()
        {
            var underTest = new EnergySource(new Vector2D(50f, 50f), 10f);
            var grid = new float[100, 100];
            grid.Set(1f);
            var output = underTest.ApplyTo(grid);
            output[50, 50].Should().Approximate(11f);
        }
        [Test]
        public void ApplyTo_DoesNot_Change_Input()
        {
            var underTest = new EnergySource(new Vector2D(50f, 50f), 100f);
            var grid = new float[100, 100];
            underTest.ApplyTo(grid);
            grid.ShouldAllBe(0f);
        }
        [Test]
        public void At_Returns_New_Instance_With_Equal_Intensity()
        {
            var underTest = new EnergySource(new Vector2D(), 13f);
            underTest.At(new Vector2D(1f, 1f)).Intensity.Should().Be(13f);
        }
        [Test]
        public void At_Returns_New_Instance_With_Passed_Position()
        {
            var underTest = new EnergySource(new Vector2D(), 13f);
            underTest.At(new Vector2D(1f, 1f)).Position.Should().Approximate(1f, 1f);
        }
        [Test]
        public void Construction()
        {
            var underTest = new EnergySource(new Vector2D(2.3f, 3.1f), 17f);
            underTest.Position.Should().Approximate(2.3f, 3.1f);
            underTest.Intensity.Should().Approximate(17f);
        }
        [Test]
        public void WithIntensity_Returns_New_Instance_With_Equal_Position()
        {
            var underTest = new EnergySource(new Vector2D(1f, 2f), 13f);
            underTest.WithIntensity(17f).Position.Should().Approximate(1f, 2f);
        }
        [Test]
        public void WithIntensity_Returns_New_Instance_With_Passed_Intensity()
        {
            var underTest = new EnergySource(new Vector2D(), 13f);
            underTest.WithIntensity(17f).Intensity.Should().Be(17f);
        }
    }
}