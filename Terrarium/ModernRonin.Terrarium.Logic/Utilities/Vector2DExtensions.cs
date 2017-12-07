using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic.Utilities
{
    public static class Vector2DExtensions
    {
        public static float[,] ToEnergyDensity(this Vector2D self) => new float[(int) self.X, (int) self.Y];
    }
}