using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Rendering.Windows.Drawing
{
    public class BackgroundRenderer : ARenderer
    {
        readonly TextureDirectory mTextureDirectory;
        public BackgroundRenderer(GraphicsDevice device, SpriteBatch batch, TextureDirectory textureDirectory) :
            base(device, batch) => mTextureDirectory = textureDirectory;
        public void Render(Vector2D size)
        {
            Batch.Draw(mTextureDirectory.GrayPixel, new Rectangle(0, 0, (int) size.X, (int) size.Y), Color.White);
        }
    }
}