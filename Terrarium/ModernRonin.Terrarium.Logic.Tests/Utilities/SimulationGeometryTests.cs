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
    }
}