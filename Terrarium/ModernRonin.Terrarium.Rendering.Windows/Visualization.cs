using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ModernRonin.Terrarium.Rendering.Windows
{
    public delegate void VisualizationEventDelegate(Visualization sender);
    public class Visualization : Game
    {
        readonly GraphicsDeviceManager mDeviceManager;
        public Visualization()
        {
            mDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        public SpriteBatch Batch { get; private set; }

        public Matrix TranslationMatrix { get; set; }

        public static VisualizationEventDelegate OnLoading { get; set; }
        public static VisualizationEventDelegate OnUpdating { get; set; }
        public static VisualizationEventDelegate OnRendering { get; set; }
        void Invoke(VisualizationEventDelegate handler) => handler?.Invoke(this);
        protected override void Initialize()
        {
            IsMouseVisible = true;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            Batch = new SpriteBatch(GraphicsDevice);
            Invoke(OnLoading);
        }
        protected override void UnloadContent() { }
        protected override void Dispose(bool disposing)
        {
            if (disposing) mDeviceManager.Dispose();
            base.Dispose(disposing);
        }
        protected override void Update(GameTime gameTime)
        {
            Invoke(OnUpdating);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            Batch.Begin(transformMatrix: TranslationMatrix, blendState: BlendState.Additive);
            GraphicsDevice.Clear(Color.Black);
            Invoke(OnRendering);
            Batch.End();
        }
    }
}