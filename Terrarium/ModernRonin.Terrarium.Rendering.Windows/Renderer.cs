using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic;
using MoreLinq;

namespace ModernRonin.Terrarium.Rendering.Windows
{
    public abstract class ARenderer
    {
        protected ARenderer(GraphicsDevice device, SpriteBatch batch)
        {
            Device = device;
            Batch = batch;
        }
        protected GraphicsDevice Device { get; }
        protected SpriteBatch Batch { get; }
    }

    public class EnergyDensityRenderer : ARenderer
    {
        public EnergyDensityRenderer(GraphicsDevice device, SpriteBatch batch) : base(device, batch) { }
        public void Render(float[,] energyDensity)
        {
            var texture = ToTexture(energyDensity);
            Batch.Draw(texture, Vector2.Zero, Color.White);
        }
        Texture2D ToTexture(float[,] energyDensity)
        {
            var width = energyDensity.GetLength(0);
            var height = energyDensity.GetLength(1);
            var result = new Texture2D(Device, width, height, false, SurfaceFormat.Color);
            var colorData = new Color[width * height];
            for (var x = 0; x < width; ++x)
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
    }

    public class BackgroundRenderer : ARenderer
    {
        readonly TextureDirectory mTextureDirectory;
        public BackgroundRenderer(GraphicsDevice device, SpriteBatch batch, TextureDirectory textureDirectory) :
            base(device, batch) => mTextureDirectory = textureDirectory;
        public void Render(Vector2D size)
        {
            Batch.Draw(mTextureDirectory.GrayPixel, new Rectangle(0, 0, (int) size.X, (int) size.Y), Color.White);
        }
    }

    public class EntityRenderer : ARenderer
    {
        readonly EntitySpriteFactory mFactory;
        public EntityRenderer(GraphicsDevice device, SpriteBatch batch, EntitySpriteFactory factory) : base(device,
            batch) => mFactory = factory;
        public void Render(IEnumerable<Entity> entities)
        {
            entities.ForEach(Draw);
        }
        void Draw(Entity entity)
        {
            var texture = mFactory.GetTextureForEntity(entity);

            Batch.Draw(texture, entity.AbsoluteBoundingBox.ToRectangle(), Color.White);
        }
    }

    public class Renderer
    {
        readonly BackgroundRenderer mBackgroundRenderer;
        readonly EnergyDensityRenderer mEnergyDensityRenderer;
        readonly EntityRenderer mEntityRenderer;
        public Renderer(
            EnergyDensityRenderer energyDensityRenderer,
            BackgroundRenderer backgroundRenderer,
            EntityRenderer entityRenderer)
        {
            mEnergyDensityRenderer = energyDensityRenderer;
            mBackgroundRenderer = backgroundRenderer;
            mEntityRenderer = entityRenderer;
        }
        public void Render(ISimulationState simulationState)
        {
            mBackgroundRenderer.Render(simulationState.Size);
            mEnergyDensityRenderer.Render(simulationState.EnergyDensity);
            mEntityRenderer.Render(simulationState.Entities);
        }
    }
}