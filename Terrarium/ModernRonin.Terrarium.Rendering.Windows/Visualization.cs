﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModernRonin.Terrarium.Logic;

namespace ModernRonin.Terrarium.Rendering.Windows
{
    public class Visualization : Game
    {
        readonly Camera mCamera = new Camera();
        readonly GraphicsDeviceManager mGraphics;
        readonly EntitySpriteFactory mEntitySpriteFactory;
        readonly TextureDirectory mTextureDirectory = new TextureDirectory();
        CameraController mCameraController;
        SpriteBatch mSpriteBatch;
        public Visualization()
        {
            mGraphics = new GraphicsDeviceManager(this);
            mEntitySpriteFactory= new EntitySpriteFactory(() => GraphicsDevice, mTextureDirectory);
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
            mSpriteBatch.Begin(transformMatrix: mCamera.TranslationMatrix, blendState:BlendState.Additive);
            GraphicsDevice.Clear(Color.Black);
            new Renderer(GraphicsDevice, mSpriteBatch, mTextureDirectory, mEntitySpriteFactory).Render(SimulationState);
            mSpriteBatch.End();
        }
    }
}