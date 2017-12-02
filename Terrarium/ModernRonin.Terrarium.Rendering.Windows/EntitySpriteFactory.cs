using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic;
using MoreLinq;

namespace ModernRonin.Terrarium.Rendering.Windows
{
    public class EntitySpriteFactory : IDisposable
    {
        const int PartTextureSizeScalar = 100;
        readonly Dictionary<string, Texture2D> mCodesToTextures = new Dictionary<string, Texture2D>();
        readonly Func<GraphicsDevice> mDeviceGetter;
        readonly Point mPartTextureSize = new Point(PartTextureSizeScalar, PartTextureSizeScalar);
        readonly TextureDirectory mTextureDirectory;
        public EntitySpriteFactory(Func<GraphicsDevice> deviceGetter, TextureDirectory textureDirectory)
        {
            mDeviceGetter = deviceGetter;
            mTextureDirectory = textureDirectory;
        }
        public void Dispose() => Clear();
        void Clear() => mCodesToTextures.Keys.ToArray().ForEach(Remove);
        public Texture2D GetTextureForEntity(Entity entity)
        {
            var key = entity.Code;
            if (!mCodesToTextures.ContainsKey(key)) mCodesToTextures[key] = CreateTexture(entity);
            return mCodesToTextures[key];
        }
        public void CleanAllExcept(IEnumerable<string> codes)
        {
            var toBeDeletedKeys = mCodesToTextures.Keys.Except(codes).ToArray();
            toBeDeletedKeys.ForEach(Remove);
        }
        void Remove(string code)
        {
            mCodesToTextures[code].Dispose();
            mCodesToTextures.Remove(code);
        }
        Texture2D CreateTexture(Entity entity)
        {
            var device = mDeviceGetter();
            var boundingBox = entity.LocalBoundingBox.Normalized.ScaleBy(PartTextureSizeScalar).ToRectangle();
            var result = new RenderTarget2D(device,
                boundingBox.Width,
                boundingBox.Height,
                false,
                SurfaceFormat.Color,
                DepthFormat.Depth24);
            using (new RenderTargetScope(device, result))
            {
                using (var batch = new SpriteBatch(device))
                {
                    batch.Begin(SpriteSortMode.Immediate);
                    RenderParts(entity.Parts, batch);
                    batch.End();
                }
            }
            //SaveAsPng(result, entity.Code, boundingBox.Width, boundingBox.Height);
            return result;
        }
        static async Task SaveAsPng(Texture2D texture, string code, int width, int height)
        {
            var fileName = code.Replace("!", "-").Replace("?", "_").Replace(".", "X") + ".png";
            Debug.WriteLine($"saving {fileName} to {ApplicationData.Current.LocalFolder.Path}...");
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName);
            using (var stream = await file.OpenStreamForWriteAsync())
            {
                texture.SaveAsPng(stream, width, height);
                stream.Flush();
            }
        }
        void RenderParts(IEnumerable<Part> parts, SpriteBatch batch)
        {
            var frozen = parts as Part[] ?? parts.ToArray();
            var minX = frozen.Select(p => p.RelativePosition.X).Min();
            var minY = frozen.Select(p => p.RelativePosition.Y).Min();
            var offset = new Vector2D(-minX, -minY);
            foreach (var part in frozen)
            {
                var texture = mTextureDirectory.ForPart(part.Kind);
                var position = ((part.RelativePosition + offset) * PartTextureSizeScalar).ToPoint();
                batch.Draw(texture, new Rectangle(position, mPartTextureSize), Color.White);
            }
        }
    }
    public class 
}