using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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

            Enum.GetNames(typeof(PartKind)).ToDictionary(Enum.Parse<PartKind>, content.Load<Texture2D>)
                .ForEach(kvp => AddForPart(kvp.Key, kvp.Value));
        }
    }
}