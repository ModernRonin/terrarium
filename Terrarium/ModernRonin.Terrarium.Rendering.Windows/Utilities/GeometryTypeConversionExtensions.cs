using System;
using Microsoft.Xna.Framework;
using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Rendering.Windows.Utilities
{
    public static class GeometryTypeConversionExtensions
    {
        public static Point ToPoint(this Vector2D self) => new Point(self.X.ToInt(), self.Y.ToInt());
        public static Vector2 ToVector2(this Vector2D self) => new Vector2(self.X, self.Y);
        public static int ToInt(this float rhs) => (int) Math.Round(rhs);
        public static Rectangle ToRectangle(this Rectangle2D self)
        {
            var position = self.MinCorner.ToPoint();
            var size = self.Diagonal.ToPoint();
            return new Rectangle(position, size);
        }
    }
}