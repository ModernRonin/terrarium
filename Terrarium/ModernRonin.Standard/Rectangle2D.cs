using System;

namespace ModernRonin.Standard
{
    public struct Rectangle2D : IEquatable<Rectangle2D>
    {
        public Vector2D MinCorner { get; }
        public Vector2D MaxCorner { get; }
        public Rectangle2D(Vector2D minCorner, Vector2D maxCorner)
        {
#if DEBUG
            if (minCorner.X > maxCorner.X || minCorner.Y > maxCorner.Y)
                throw new ArgumentOutOfRangeException(nameof(minCorner),
                    $"{nameof(minCorner)} must be smaller or equal than {nameof(maxCorner)}");
#endif
            MinCorner = minCorner;
            MaxCorner = maxCorner;
        }
        public float Width => MaxCorner.X - MinCorner.X;
        public float Height => MaxCorner.Y - MinCorner.Y;
        public Vector2D Diagonal => MaxCorner - MinCorner;
        public Rectangle2D Normalized => new Rectangle2D(Vector2D.Zero, new Vector2D(Width, Height));
        public Rectangle2D ScaleBy(float factor) => new Rectangle2D(MinCorner, MinCorner+ factor*Diagonal);
        #region Equality
        public bool Equals(Rectangle2D other) => MinCorner.Equals(other.MinCorner) && MaxCorner.Equals(other.MaxCorner);
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Rectangle2D && Equals((Rectangle2D) obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return MinCorner.GetHashCode() * 397 ^ MaxCorner.GetHashCode();
            }
        }
        public static bool operator ==(Rectangle2D left, Rectangle2D right) => left.Equals(right);
        public static bool operator !=(Rectangle2D left, Rectangle2D right) => !left.Equals(right);
        #endregion
        public Rectangle2D RelativeTo(Vector2D center) => new Rectangle2D(center + MinCorner, center + MaxCorner);
    }
}