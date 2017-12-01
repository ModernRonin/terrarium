using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic;
using MoreLinq;

namespace ModernRonin.Terrarium.Rendering.Windows
{
    public class Renderer
    {
        readonly GraphicsDevice mGraphicsDevice;
        readonly SpriteBatch mSpriteBatch;
        readonly TextureDirectory mTextureDirectory;
        public Renderer(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, TextureDirectory textureDirectory)
        {
            mGraphicsDevice = graphicsDevice;
            mSpriteBatch = spriteBatch;
            mTextureDirectory = textureDirectory;
        }
        public void Render(ISimulationState simulationState)
        {
            DrawBackground(simulationState.Size);
            DrawEnergyDensity(simulationState.EnergyDensity);
            DrawEntities(simulationState.Entities);
        }
        void DrawEnergyDensity(float[,] energyDensity)
        {
            var texture = ToTexture(energyDensity);
            mSpriteBatch.Draw(texture, Vector2.Zero, Color.White);
        }
        Texture2D ToTexture(float[,] energyDensity)
        {
            var width = energyDensity.GetLength(0);
            var height = energyDensity.GetLength(1);
            var result= new Texture2D(mGraphicsDevice, width, height, false, SurfaceFormat.Color);
            var colorData= new Color[width* height];
            for (var x=0; x<width; ++x)
            for (var y = 0; y < height; ++y)
            {
                var index = x + y * width;
                var value = energyDensity[x, y];
                var alpha = MapToOpacity(value);
                var color = Color.Yellow;
                color.A = alpha;
                colorData[index] = color;
            }
            result.SetData(colorData);
            return result;
        }
        static byte MapToOpacity(float value)
        {
            const byte factor = 5;
            var result = factor * value;
            if (result > 225) return 255;
            return (byte) result;
        }
        void DrawEntities(IEnumerable<Entity> entities)
        {
            entities.ForEach(Draw);
        }
        void DrawBackground(Vector2D size)
        {
            mSpriteBatch.Draw(mTextureDirectory.GrayPixel,
                new Rectangle(0, 0, (int) size.X, (int) size.Y),
                Color.White);
        }
        void Draw(Entity entity)
        {
            void drawSprite(Sprite sprite) => mSpriteBatch.Draw(sprite.Image, sprite.BoundingBox, Color.White);
            entity.Parts.Select(p => ToSprite(p, entity.Position)).ForEach(drawSprite);
        }
        Sprite ToSprite(Part part, Vector2D origin)
        {
            var absolutePosition = origin + part.RelativePosition;
            return new Sprite
            {
                Image = mTextureDirectory.ForPart(part.Kind),
                BoundingBox = new Rectangle(absolutePosition.ToPoint(), new Point(1, 1))
            };
        }

        class Sprite
        {
            public Rectangle BoundingBox { get; set; }
            public Texture2D Image { get; set; }
        }
    }

    public class EntitySprite
    {
        public EntitySprite(GraphicsDevice device, TextureDirectory textureDirectory, Entity entity)
        {
            Code = entity.Code;

            //var renderTarget= new RenderTarget2D(device, );
            
        }
        public string Code { get; }
    }
}