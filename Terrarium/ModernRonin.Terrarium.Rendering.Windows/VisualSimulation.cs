using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModernRonin.Terrarium.Logic;

namespace ModernRonin.Terrarium.Rendering.Windows
{
    public class VisualSimulation : Game
    {
        readonly Camera mCamera = new Camera();
        readonly GraphicsDeviceManager mGraphics;
        readonly TextureDirectory mTextureDirectory = new TextureDirectory();
        CameraController mCameraController;
        SpriteBatch mSpriteBatch;
        public VisualSimulation()
        {
            mGraphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        public Func<ISimulationState> OnUpdate { get; set; } = () => new SimulationState();
        ISimulationState SimulationState { get; set; }
        protected override void Initialize()
        {
            Window.ClientSizeChanged += (_, __) =>
            {
                mCamera.ViewportWidth = GraphicsDevice.Viewport.Width;
                mCamera.ViewportHeight = GraphicsDevice.Viewport.Height;
            };
            IsMouseVisible = true;
            mCameraController = new CameraController(mCamera);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            mSpriteBatch = new SpriteBatch(GraphicsDevice);
            mTextureDirectory.Load(Content);
        }
        protected override void UnloadContent() { }
        protected override void Update(GameTime gameTime)
        {
            SimulationState = OnUpdate();
            mCameraController.Update();
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            mSpriteBatch.Begin(transformMatrix: mCamera.TranslationMatrix);
            GraphicsDevice.Clear(Color.Black);
            new Renderer(mSpriteBatch, mTextureDirectory).Render(SimulationState);
            mSpriteBatch.End();
        }
    }
}