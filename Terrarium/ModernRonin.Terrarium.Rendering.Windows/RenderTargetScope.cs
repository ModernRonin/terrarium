using System;
using Microsoft.Xna.Framework.Graphics;

namespace ModernRonin.Terrarium.Rendering.Windows
{
    public class RenderTargetScope : IDisposable
    {
        readonly GraphicsDevice mDevice;
        public RenderTargetScope(GraphicsDevice device, RenderTarget2D renderTarget)
        {
            mDevice = device;
            mDevice.SetRenderTarget(renderTarget);
        }
        public void Dispose()
        {
            mDevice.SetRenderTarget(null);
        }
    }
}