using System.Windows;

namespace SimulationView
{
    public static class VectorExtensions
    {
        public static Vector ScaleBy(this Vector self, Vector scale) => new Vector(self.X * scale.X, self.Y * scale.Y);
        public static Vector ScaleTo(this Size self, Size destination) =>
            new Vector(destination.Width / self.Width, destination.Height / self.Height);
        public static Vector WrapOver(this Vector self, Size size)
        {
            double clamp(double value, double limit)
            {
                var result = value % limit;
                if (result < 0) result += limit;
                return result;
            }

            var x = clamp(self.X, size.Width);
            var y = clamp(self.Y, size.Height);
            return new Vector(x, y);
        }
        public static Vector Normalized(this Vector self)
        {
            var result = self;
            result.Normalize();
            return result;
        }
    }
}