using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic.Objects.Entities;
using MoreLinq;

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
        public void Load(ContentManager content)
        {
            GrayPixel = content.Load<Texture2D>("GreyPoint");

            EnumerableExtensions.EnumToDictionary<PartKind, Texture2D>(k => content.Load<Texture2D>(k.ToString()))
                                .ForEach(kvp => AddForPart(kvp.Key, kvp.Value));
        }
    }
}