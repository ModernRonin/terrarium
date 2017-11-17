using System.Windows;
using ModernRonin.Standard;

namespace SimulationView
{
    public static class Vector2Adaptations
    {
        public static Size ToSize(this Vector2D self) => new Size(self.X, self.Y);
        public static Vector2D ToVector2D(this Size self) => new Vector2D((float) self.Width, (float) self.Height);
        public static Vector ToVector(this Vector2D self) => new Vector(self.X, self.Y);
        public static Point ToPoint(this Vector2D self) => new Point(self.X, self.Y);
        public static Vector2D ScaleTo(this Vector2D self, Size destination) => self.ScaleTo(destination.ToVector2D());
    }
}