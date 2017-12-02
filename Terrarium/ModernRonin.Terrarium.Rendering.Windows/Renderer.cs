﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic;
using MoreLinq;

namespace ModernRonin.Terrarium.Rendering.Windows
{
    public abstract class ARenderer
    {
        protected GraphicsDevice Device { get; }
        protected SpriteBatch Batch { get; }
        protected ARenderer(GraphicsDevice device, SpriteBatch batch)
        {
            Device = device;
            Batch = batch;
        }
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
            return (byte)result;
        }
    }
    public class BackgroundRenderer : ARenderer {
        readonly TextureDirectory mTextureDirectory;
        public BackgroundRenderer(GraphicsDevice device, SpriteBatch batch, TextureDirectory textureDirectory) : base(device, batch)
        {
            mTextureDirectory = textureDirectory;
        }
        public void Render(Vector2D size)
        {
            Batch.Draw(mTextureDirectory.GrayPixel,
                new Rectangle(0, 0, (int)size.X, (int)size.Y),
                Color.White);
        }
    }
    public class Renderer : ARenderer
    {
        readonly EntitySpriteFactory mFactory;
        readonly EnergyDensityRenderer mEnergyDensityRenderer;
        readonly BackgroundRenderer mBackgroundRenderer;
        public Renderer(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch, TextureDirectory textureDirectory, EntitySpriteFactory factory)
            :base(graphicsDevice, spriteBatch)
        {
            mEnergyDensityRenderer= new EnergyDensityRenderer(Device, Batch);
            mBackgroundRenderer= new BackgroundRenderer(Device, Batch, textureDirectory);
            mFactory = factory;
        }
        public void Render(ISimulationState simulationState)
        {
            mBackgroundRenderer.Render(simulationState.Size);
            mEnergyDensityRenderer.Render(simulationState.EnergyDensity);
            DrawEntities(simulationState.Entities);
        }
        void DrawEntities(IEnumerable<Entity> entities)
        {
            entities.ForEach(Draw);
        }
        
        void Draw(Entity entity)
        {
            var texture = mFactory.GetTextureForEntity(entity);

            Batch.Draw(texture, entity.AbsoluteBoundingBox.ToRectangle(), Color.White);
        }

    }
}