using System;
using System.Linq;
using ModernRonin.Standard;
using ModernRonin.Standard.Tests;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests
{
    public static class Vector2DExtensions
    {
        
    }
    [TestFixture]
    public class EnergySourceTests
    {
        [Test]
        public void ApplyTo_Adds_Full_Intensity_At_Position()
        {
            var output = RunStandardScenario();
            output[50, 50].Should().Approximate(11f);
        }
        static float[,] RunStandardScenario()
        {
            var underTest = new EnergySource(new Vector2D(50f, 50f), 10f);
            var grid = new float[100, 100];
            grid.Set(1f);
            var output = underTest.ApplyTo(grid);
            return output;
        }
        static bool HasDistanceFromStandardPosition(int x, int y, int distance)
        {
            var actualDistance = GetDistanceFromStandardPosition(x, y);
            return Math.Abs(actualDistance - distance) < 0.001f;
        }
        static double GetDistanceFromStandardPosition(int x, int y)
        {
            var dx = 50 - x;
            var dy = 50 - y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
        [Test]
        public void ApplyTo_Adds_IntensityMinusOne_At_All_Positions_With_DistanceOne_From_Center()
        {
            var output = RunStandardScenario();
            bool hasRightDistance(int x, int y) => HasDistanceFromStandardPosition(x, y, 1);
            output.Where(hasRightDistance).ShouldAllApproximate(10f);
        }
        [Test]
        public void ApplyTo_Adds_One_At_All_Positions_With_DistanceIntensityMinusOne_From_Center()
        {
            var output = RunStandardScenario();
            bool hasRightDistance(int x, int y) => HasDistanceFromStandardPosition(x, y, 9);
            output.Where(hasRightDistance).ShouldAllApproximate(2f);
        }
        [Test]
        public void ApplyTo_DoesNot_Change_Any_Positions_With_DistanceGreaterOrEqualIntensity_From_Center()
        {
            var output = RunStandardScenario();
            bool hasRightDistance(int x, int y) => 10f <= GetDistanceFromStandardPosition(x, y);
            output.Where(hasRightDistance).ShouldAllApproximate(1f);
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