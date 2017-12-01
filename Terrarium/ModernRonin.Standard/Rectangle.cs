using System;

namespace ModernRonin.Standard
{
    public struct Rectangle : IEquatable<Rectangle>
    {
        public Vector2D MinCorner { get; }
        public Vector2D MaxCorner { get; }
        public Rectangle(Vector2D minCorner, Vector2D maxCorner)
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
        #region Equality
        public bool Equals(Rectangle other) => MinCorner.Equals(other.MinCorner) && MaxCorner.Equals(other.MaxCorner);
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Rectangle && Equals((Rectangle) obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return MinCorner.GetHashCode() * 397 ^ MaxCorner.GetHashCode();
            }
        }
        public static bool operator ==(Rectangle left, Rectangle right) => left.Equals(right);
        public static bool operator !=(Rectangle left, Rectangle right) => !left.Equals(right);
        #endregion
    }
}