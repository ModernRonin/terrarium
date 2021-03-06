﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using ModernRonin.Terrarium.Logic.Objects.Entities;

namespace ModernRonin.Terrarium.Rendering.Windows.Drawing
{
    public interface IEntitySpriteFactory : IDisposable
    {
        Texture2D GetTextureForEntity(IEntityState entityState);
        void CleanAllExcept(IEnumerable<string> codes);
    }
}