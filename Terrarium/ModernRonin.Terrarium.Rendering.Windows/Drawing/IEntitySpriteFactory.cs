using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using ModernRonin.Terrarium.Logic;

namespace ModernRonin.Terrarium.Rendering.Windows.Drawing
{
    public interface IEntitySpriteFactory : IDisposable
    {
        void Dispose();
        Texture2D GetTextureForEntity(Entity entity);
        void CleanAllExcept(IEnumerable<string> codes);
    }
}