﻿using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModernRonin.Terrarium.Logic;
using MoreLinq;

namespace ModernRonin.Terrarium.Rendering.Windows
{
    public class EntitySprite : IDisposable
    {
        readonly RenderTarget2D mRenderTarget;
        public EntitySprite(GraphicsDevice device, TextureDirectory textureDirectory, Entity entity)
        {
            Code = entity.Code;
            var localBoundingBox = entity.LocalBoundingBox;
            mRenderTarget = new RenderTarget2D(device,
                localBoundingBox.Width.ToInt(),
                localBoundingBox.Height.ToInt(),
                false,
                SurfaceFormat.Color,
                DepthFormat.Depth24);

            device.SetRenderTarget(mRenderTarget);
            using (var batch = new SpriteBatch(device))
            {
                batch.Begin();

                void drawSprite(Renderer.PartSprite sprite) =>
                    batch.Draw(sprite.Image, sprite.BoundingBox, Color.White);

                Renderer.PartSprite toSprite(Part part) => new Renderer.PartSprite
                {
                    Image = textureDirectory.ForPart(part.Kind),
                    BoundingBox = new Rectangle(part.RelativePosition.ToPoint(), new Point(1, 1))
                };

                entity.Parts.Select(toSprite).ForEach(drawSprite);

                batch.End();
            }
            device.SetRenderTarget(null);
        }
        public Texture2D Texture => mRenderTarget;
        public string Code { get; }
        public void Dispose()
        {
            mRenderTarget.Dispose();
        }
    }
}