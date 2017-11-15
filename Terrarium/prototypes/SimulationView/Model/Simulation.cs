using System;
using System.Collections.Generic;

namespace SimulationView.Model
{
    public class Simulation
    {
        public int Width { get; } = 100;
        public int Height { get; } = 100;

    }

    public class Entity
    {
        public IList<Part> Parts { get; set; }
    }

    public class Part
    {
        public Vector2D RelativePosition { get; set; }
        public PartKind	 Kind { get; set; }
    }

    public enum PartKind
    {
        Core,
        Absorber,
        Store
    }
    public struct Vector2D : IEquatable<Vector2D>
    {
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
                return (DeltaX * 397) ^ DeltaY;
            }
        }
        public static bool operator ==(Vector2D left, Vector2D right) => left.Equals(right);
        public static bool operator !=(Vector2D left, Vector2D right) => !left.Equals(right);
    }
}