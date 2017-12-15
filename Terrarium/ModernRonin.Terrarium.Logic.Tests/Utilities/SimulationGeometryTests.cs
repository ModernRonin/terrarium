using FluentAssertions;
using ModernRonin.Standard;
using ModernRonin.Standard.Tests;
using ModernRonin.Terrarium.Logic.Utilities;
using NUnit.Framework;

namespace ModernRonin.Terrarium.Logic.Tests.Utilities
{
    [TestFixture]
    public class SimulationGeometryTests
    {
        [TestCase(0, 0, -1)]
        [TestCase(4, 0, 1)]
        [TestCase(7, -1, 1)]
        [TestCase(8, 0, -1)]
        [TestCase(15, -1, 1)]
        [TestCase(16, 0, -1)]
        [TestCase(23, -1, 1)]
        public void VectorFor(int directionIndex, int expectedX, int expectedY)
        {
            var output = SimulationGeometry.VectorFor(directionIndex);
            output.OughtTo().Approximate(expectedX, expectedY);
        }
        [Test]
        public void FindNextUnoccupiedPoint_Returns_First_Non_Occupied_Point_In_Direction()
        {
            var occupied = new[]
            {
                new Rectangle2D(new Vector2D(1, 1), new Vector2D(4, 4)),
                new Rectangle2D(new Vector2D(0, -1), new Vector2D(2, 3))
            };
            var start = new Vector2D(0, 0);
            SimulationGeometry.FindNextUnoccupiedPoint(start, Vector2D.One, occupied).OughtTo().Approximate(4, 4);
        }
        [Test]
        public void FindNextUnoccupiedPoint_Returns_Start_If_None_Of_Occupied_Contains_Start()
        {
            var occupied = new[]
            {
                new Rectangle2D(new Vector2D(1, 1), new Vector2D(4, 4)),
                new Rectangle2D(new Vector2D(0, -1), new Vector2D(2, 3))
            };
            var start = new Vector2D(-2, -2);
            SimulationGeometry.FindNextUnoccupiedPoint(start, Vector2D.One, occupied).Should().Be(start);
        }
        [Test]
        public void PointsFromTo_Returns_NoPointsAtAll_If_Start_Equals_End()
        {
            SimulationGeometry.PointsFromTo(Vector2D.Zero, Vector2D.One, Vector2D.Zero).Should().BeEmpty();
        }
        [Test]
        public void PointsFromTo_Returns_Steps_From_InclusiveStart_To_ExclusiveEnd()
        {
            SimulationGeometry.PointsFromTo(Vector2D.Zero, Vector2D.One, new Vector2D(4, 4)).Should()
                              .Equal(Vector2D.Zero, Vector2D.One, new Vector2D(2, 2), new Vector2D(3, 3));
        }
    }
}