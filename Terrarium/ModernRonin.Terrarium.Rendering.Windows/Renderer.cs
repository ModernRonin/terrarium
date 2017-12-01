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
        readonly EntitySpriteFactory mFactory;
        public Renderer(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, TextureDirectory textureDirectory, EntitySpriteFactory factory)
        {
            mGraphicsDevice = graphicsDevice;
            mSpriteBatch = spriteBatch;
            mTextureDirectory = textureDirectory;
            mFactory = factory;
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
        void DrawBackground(Vector2D size)
        {
            mSpriteBatch.Draw(mTextureDirectory.GrayPixel,
                new Rectangle(0, 0, (int)size.X, (int)size.Y),
                Color.White);
        }
        void DrawEntities(IEnumerable<Entity> entities)
        {
            entities.ForEach(Draw);
        }
        
        void Draw(Entity entity)
        {
            var texture = mFactory.GetTextureForEntity(entity);

            mSpriteBatch.Draw(texture, entity.Position.ToVector2(), Color.White);
        }

    }
}