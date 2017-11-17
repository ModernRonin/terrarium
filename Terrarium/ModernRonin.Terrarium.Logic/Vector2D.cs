using System;

namespace ModernRonin.Terrarium.Logic
{
    public struct Vector2D : IEquatable<Vector2D>
    {
        public float X { get; }
        public float Y { get; }
        public Vector2D(float x, float y)
        {
            X = x;
            Y = y;
        }
        #region Equality
        public bool Equals(Vector2D other) => X.Equals(other.X) && Y.Equals(other.Y);
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vector2D && Equals((Vector2D) obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return X.GetHashCode() * 397 ^ Y.GetHashCode();
            }
        }
        public static bool operator ==(Vector2D left, Vector2D right) => left.Equals(right);
        public static bool operator !=(Vector2D left, Vector2D right) => !left.Equals(right);
        #endregion
        public static Vector2D operator +(Vector2D lhs, Vector2D rhs) => new Vector2D(lhs.X + rhs.X, lhs.Y + rhs.Y);
        public static Vector2D operator -(Vector2D lhs, Vector2D rhs) => new Vector2D(lhs.X - rhs.X, lhs.Y - rhs.Y);
        public static Vector2D operator *(Vector2D lhs, float scalar) => new Vector2D(scalar * lhs.X, scalar * lhs.Y);
        public static Vector2D operator *(float scalar, Vector2D rhs) => new Vector2D(scalar * rhs.X, scalar * rhs.Y);
        public static Vector2D operator /(Vector2D lhs, float scalar) => new Vector2D(scalar / lhs.X, scalar / lhs.Y);
        public Vector2D ScaleBy(Vector2D scale) => new Vector2D(X * scale.X, Y * scale.Y);
        public Vector2D ScaleTo(Vector2D destination) => new Vector2D(destination.X / X, destination.Y / Y);
        public Vector2D ClampWithin(Vector2D maximum)
        {
            float clamp(float value, float limit)
            {
                var result = value % limit;
                if (result < 0) result += limit;
                return result;
            }

            var x = clamp(X, maximum.X);
            var y = clamp(Y, maximum.Y);
            return new Vector2D(x, y);
        }
        public float LengthSquared => X * X + Y * Y;
        public float Length => (float) Math.Sqrt(LengthSquared);
        public Vector2D Normalized
        {
            get
            {
                var l = Length;
                if (Math.Abs(l) < 0.001) return Zero;
                return this / l;
            }
        }
        public static Vector2D Zero => new Vector2D(0, 0);
    }
}