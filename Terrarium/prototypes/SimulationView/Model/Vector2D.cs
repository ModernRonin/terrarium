using System;

namespace SimulationView.Model
{
    public struct Vector2D : IEquatable<Vector2D>
    {
        public static Vector2D Zero => new Vector2D(0, 0);
        public int DeltaX { get; }
        public int DeltaY { get; }
        public Vector2D(int deltaX, int deltaY)
        {
            DeltaX = deltaX;
            DeltaY = deltaY;
        }
        public bool Equals(Vector2D other) => DeltaX == other.DeltaX && DeltaY == other.DeltaY;
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vector2D && Equals((Vector2D) obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return DeltaX * 397 ^ DeltaY;
            }
        }
        public static bool operator ==(Vector2D left, Vector2D right) => left.Equals(right);
        public static bool operator !=(Vector2D left, Vector2D right) => !left.Equals(right);
    }
}