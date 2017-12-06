using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic
{
    public interface IEnergySource
    {
        Vector2D Position { get; }
        float Intensity { get; }
        Vector2D Speed { get; }
        EnergySource At(Vector2D newPosition);
        EnergySource WithIntensity(float newIntensity);
        float[,] ApplyTo(float[,] grid);
    }
}