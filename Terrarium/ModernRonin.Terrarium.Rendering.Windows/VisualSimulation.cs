using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic;
using MoreLinq;

namespace ModernRonin.Terrarium.Rendering.Windows
{
    public class VisualSimulation : Game
    {
        const int ScalingFactor = 100;
        const float ZoomSpeed = 0.0001f;
        const int PanSpeed = 5;
        readonly Camera mCamera = new Camera();
        readonly GraphicsDeviceManager mGraphics;
        readonly Dictionary<PartKind, Texture2D> mPartKindTextures = new Dictionary<PartKind, Texture2D>();
        readonly Point mScalingVector = new Point(ScalingFactor, ScalingFactor);
        Texture2D mGrayPixel;
        KeyboardState mLastKeyboardState;
        MouseState mLastMouseState;
        SpriteBatch mSpriteBatch;
        public VisualSimulation()
        {
            mGraphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        public Func<ISimulationState> OnUpdate { get; set; } = () => new SimulationState();
        ISimulationState SimulationState { get; set; }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            SetCameraViewport();
            Window.ClientSizeChanged += (_, __) => SetCameraViewport();
            IsMouseVisible = true;
            mLastMouseState = Mouse.GetState();
            mLastKeyboardState = Keyboard.GetState();
            base.Initialize();
        }
        void SetCameraViewport()
        {
            mCamera.ViewportWidth = GraphicsDevice.Viewport.Width;
            mCamera.ViewportHeight = GraphicsDevice.Viewport.Height;
        }
        protected override void LoadContent()
        {
            mSpriteBatch = new SpriteBatch(GraphicsDevice);

            Enum.GetNames(typeof(PartKind)).ToDictionary(Enum.Parse<PartKind>, Content.Load<Texture2D>)
                .ForEach(kvp => mPartKindTextures.Add(kvp.Key, kvp.Value));
            mGrayPixel = Content.Load<Texture2D>("GreyPoint");
        }
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            SimulationState = OnUpdate();

            var currentMouseState = Mouse.GetState();
            var currentKeyboardState = Keyboard.GetState();
            if (currentMouseState.LeftButton == ButtonState.Pressed)
            {
                var dx = currentMouseState.X - mLastMouseState.X;
                var dy = currentMouseState.Y - mLastMouseState.Y;
                var movement = new Vector2(-dx*PanSpeed, -dy*PanSpeed);
                mCamera.MoveCamera(movement);
            }
            var zoom = ZoomSpeed * (currentMouseState.ScrollWheelValue - mLastMouseState.ScrollWheelValue);
            mCamera.AdjustZoom(zoom);
            if (mLastKeyboardState.IsKeyDown(Keys.C) && currentKeyboardState.IsKeyUp(Keys.C))
                mCamera.CenterOn(new Vector2(50, 50) * ScalingFactor);

            mLastMouseState = currentMouseState;
            mLastKeyboardState = currentKeyboardState;
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            mSpriteBatch.Begin(transformMatrix: mCamera.TranslationMatrix);
            GraphicsDevice.Clear(Color.Black);
            mSpriteBatch.Draw(mGrayPixel,
                new Rectangle(0,
                    0,
                    (int) (ScalingFactor * SimulationState.Size.X),
                    (int) (ScalingFactor * SimulationState.Size.Y)),
                Color.DarkGray);
            SimulationState.Entities.ForEach(Draw);
            mSpriteBatch.End();
        }
        void Draw(Entity entity)
        {
            void drawSprite(Sprite sprite) => mSpriteBatch.Draw(sprite.Image, sprite.BoundingBox, Color.White);
            entity.Parts.Select(p => ToSprite(p, entity.Position)).ForEach(drawSprite);
        }
        Sprite ToSprite(Part part, Vector2D origin)
        {
            var absolutePosition = (origin + part.RelativePosition) * ScalingFactor;
            return new Sprite
            {
                Image = mPartKindTextures[part.Kind],
                BoundingBox = new Rectangle(absolutePosition.ToPoint(), mScalingVector)
            };
        }

        class Sprite
        {
            public Rectangle BoundingBox { get; set; }
            public Texture2D Image { get; set; }
        }
    }
}