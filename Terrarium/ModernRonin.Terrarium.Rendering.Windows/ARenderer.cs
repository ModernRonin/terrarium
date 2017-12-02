using Microsoft.Xna.Framework.Graphics;

namespace ModernRonin.Terrarium.Rendering.Windows
{
    public abstract class ARenderer
    {
        protected ARenderer(GraphicsDevice device, SpriteBatch batch)
        {
            Device = device;
            Batch = batch;
        }
        protected GraphicsDevice Device { get; }
        protected SpriteBatch Batch { get; }
    }
}