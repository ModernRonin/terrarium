using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ModernRonin.Terrarium.Rendering.Windows.Drawing
{
    public class EnergyDensityRenderer : ARenderer
    {
        public EnergyDensityRenderer(GraphicsDevice device, SpriteBatch batch) : base(device, batch) { }
        public void Render(float[,] energyDensity)
        {
            var texture = ToTexture(energyDensity);
            Batch.Draw(texture, Vector2.Zero, Color.White);
        }
        Texture2D ToTexture(float[,] energyDensity)
        {
            var width = energyDensity.GetLength(0);
            var height = energyDensity.GetLength(1);
            var result = new Texture2D(Device, width, height, false, SurfaceFormat.Color);
            var colorData = new Color[width * height];
            for (var x = 0; x < width; ++x)
            for (var y = 0; y < height; ++y)
            {
                var index = x + y * width;
                var value = energyDensity[x, y];
                var alpha = MapToOpacity(value);
                var color = Color.Yellow;
                color.A = alpha;
                colorData[index] = color;
            }
            result.SetData(colorData);
            return result;
        }
        static byte MapToOpacity(float value)
        {
            const byte factor = 5;
            var result = factor * value;
            if (result > 225) return 255;
            return (byte) result;
        }
    }
}