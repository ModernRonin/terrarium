using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using ModernRonin.Terrarium.Logic;

namespace ModernRonin.Terrarium.Rendering.Windows
{
    public class TextureDirectory
    {
        readonly Dictionary<PartKind, Texture2D> mPartKindTextures = new Dictionary<PartKind, Texture2D>();
        public Texture2D GrayPixel { get; set; }
        public void AddForPart(PartKind partKind, Texture2D texture)
        {
            mPartKindTextures[partKind] = texture;
        }
        public Texture2D ForPart(PartKind partKind) => mPartKindTextures[partKind];
    }
}