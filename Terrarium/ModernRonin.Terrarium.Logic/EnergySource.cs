using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic
{
    public class EnergySource : IEnergySource
    {
        public Vector2D Position { get; }
        public float Intensity { get; }
        public Vector2D Speed { get; }
        public EnergySource(Vector2D position, float intensity, Vector2D speed)
        {
            Position = position;
            Intensity = intensity;
            Speed = speed;
        }
        public EnergySource(Vector2D position, float intensity) : this(position, intensity, Vector2D.Zero) { }
        public EnergySource At(Vector2D newPosition) => new EnergySource(newPosition, Intensity, Speed);
        public EnergySource WithIntensity(float newIntensity) => new EnergySource(Position, newIntensity, Speed);
        public float[,] ApplyTo(float[,] grid)
        {
            var result = (float[,]) grid.Clone();
            void set(Vector2D where, float value) => result[(int) where.X, (int) where.Y] = value;
            float get(Vector2D where) => grid[(int) where.X, (int) where.Y];
            void add(Vector2D where, float delta) => set(where, get(where) + delta);

            for (var x = 0; x < grid.GetLength(0); ++x)
            for (var y = 0; y < grid.GetLength(1); ++y)
            {
                var otherPosition = new Vector2D(x, y);
                var distance = (Position - otherPosition).Length;
                var toBeAdded = Intensity - distance;
                if (toBeAdded > 0) add(otherPosition, toBeAdded);
            }
            return result;
        }
    }
}