using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using ModernRonin.Terrarium.Logic;

namespace ModernRonin.Terrarium.Rendering.Windows.Drawing
{
    public interface IEntitySpriteFactory : IDisposable
    {
        Texture2D GetTextureForEntity(EntityState entityState);
        void CleanAllExcept(IEnumerable<string> codes);
    }
}