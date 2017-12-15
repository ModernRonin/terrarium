using System.Collections.Generic;
using System.Linq;
using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic.Utilities
{
    /// <summary>
    /// Geometric operations different from what a standard/classical euclidean geometry library would do or how it would do them
    /// </summary>
    public static class SimulationGeometry
    {
        static readonly Vector2D[] sDirectionVectors =
        {
            new Vector2D(0, -1), new Vector2D(1, -1), new Vector2D(1, 0), new Vector2D(1, 1), new Vector2D(0, 1),
            new Vector2D(-1, 0), new Vector2D(-1, -1), new Vector2D(-1, 1)
        };
        public static Vector2D VectorFor(int directionIndex) =>
            sDirectionVectors[directionIndex % sDirectionVectors.Length];
        // ReSharper disable once ParameterTypeCanBeEnumerable.Global
        public static Vector2D FindNextUnoccupiedPoint(Vector2D start, Vector2D direction, Rectangle2D[] occupied)
        {
            if (!occupied.Any(o => o.Contains(start))) return start;
            return FindNextUnoccupiedPoint(start + direction, direction, occupied);
        }
        public static IEnumerable<Vector2D> PointsFromTo(Vector2D start, Vector2D increment, Vector2D end)
        {
            while (start != end)
            {
                yield return start;
                start += increment;
            }
        }
    }
}