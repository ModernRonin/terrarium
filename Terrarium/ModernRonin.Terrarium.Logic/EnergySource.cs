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
            grid[(int) Position.X, (int) Position.Y] = Intensity;
            return grid;
        }
    }
}