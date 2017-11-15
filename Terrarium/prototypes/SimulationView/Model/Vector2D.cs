using System;

namespace SimulationView.Model
{
    public struct Vector2D : IEquatable<Vector2D>
    {
        public static Vector2D Zero => new Vector2D(0, 0);
        public int X { get; }
        public int Y { get; }
        public Vector2D(int x, int y)
        {
            X = x;
            Y = y;
        }
        public bool Equals(Vector2D other) => X == other.X && Y == other.Y;
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vector2D && Equals((Vector2D) obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return X * 397 ^ Y;
            }
        }
        public static bool operator ==(Vector2D left, Vector2D right) => left.Equals(right);
        public static bool operator !=(Vector2D left, Vector2D right) => !left.Equals(right);
        public static Vector2D operator *(Vector2D vector, int scalar) =>
            new Vector2D(scalar * vector.X, scalar * vector.Y);
    }
}