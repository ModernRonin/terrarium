using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModernRonin.Standard;
using ModernRonin.Terrarium.Logic;
using MoreLinq;

namespace ModernRonin.Terrarium.Rendering.Windows
{
    public class VisualSimulation : Game
    {
        readonly Camera mCamera = new Camera();
        readonly TextureDirectory mTextureDirectory= new TextureDirectory();
        readonly GraphicsDeviceManager mGraphics;
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
            mTextureDirectory.GrayPixel= Content.Load<Texture2D>("GreyPoint");

            Enum.GetNames(typeof(PartKind)).ToDictionary(Enum.Parse<PartKind>, Content.Load<Texture2D>)
                .ForEach(kvp => mTextureDirectory.AddForPart(kvp.Key, kvp.Value));
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
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
            Render();
            mSpriteBatch.End();
        }
        void Render()
        {
            mSpriteBatch.Draw(mTextureDirectory.GrayPixel,
                new Rectangle(0, 0, (int) SimulationState.Size.X, (int) SimulationState.Size.Y),
                Color.DarkGray);
            SimulationState.Entities.ForEach(Draw);
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

    public class Renderer
    {
        
    }
}