using Microsoft.Xna.Framework;
using ModernRonin.Standard;

namespace MonoGameUwpXaml {
    public static class Vector2DExtensions
    {
        public static Point ToPoint(this Vector2D self) => new Point(self.X, self.Y);
    }
}