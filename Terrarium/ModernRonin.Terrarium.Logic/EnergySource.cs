using System;
using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic
{
    public struct EnergySource
    {
        public Vector2D Position { get; }
        public float Intensity { get; }
        public EnergySource(Vector2D position, float intensity)
        {
            Position = position;
            Intensity = intensity;
        }
        public EnergySource At(Vector2D newPosition) => new EnergySource(newPosition, Intensity);
        public EnergySource WithIntensity(float newIntensity) => new EnergySource(Position, newIntensity);
        public float[,] ApplyTo(float[,] grid)
        {
            var result = (float[,])grid.Clone();
            void set(Vector2D where, float value) => result[(int) where.X, (int) where.Y] = value;
            float get(Vector2D where) => grid[(int)where.X, (int)where.Y];
            void add(Vector2D where, float delta) => set(where, get(where) + delta);

            add(Position, Intensity);
            return result;
        }
    }
}