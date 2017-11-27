using System;
using Microsoft.Xna.Framework;
using ModernRonin.Standard;

namespace MonoGameUwpXaml
{
    public static class Vector2DExtensions
    {
        public static Point ToPoint(this Vector2D self) => new Point(self.X.ToInt(), self.Y.ToInt());
        static int ToInt(this float rhs) => (int) Math.Round(rhs);
    }
}