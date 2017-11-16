using System.Windows;

namespace SimulationView
{
    public static class VectorExtensions
    {
        public static Vector ScaleBy(this Vector self, Vector scale) => new Vector(self.X * scale.X, self.Y * scale.Y);
    }
}