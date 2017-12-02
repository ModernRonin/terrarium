using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ModernRonin.Terrarium.Rendering.Windows
{
    public class Visualization : Game
    {
        readonly GraphicsDeviceManager mDeviceManager;
        public Visualization()
        {
            mDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        public SpriteBatch Batch { get; private set; }
        public Action<ContentManager> OnLoading { get; set; }
        public Action OnUpdating { get; set; }
        public Func<Matrix> OnSettingTranslationMatrix { get; set; }
        public Action OnRendering { get; set; }
        protected override void Initialize()
        {
            IsMouseVisible = true;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            Batch = new SpriteBatch(GraphicsDevice);
            OnLoading(Content);
        }
        protected override void UnloadContent() { }
        protected override void Dispose(bool disposing)
        {
            if (disposing) mDeviceManager.Dispose();
            base.Dispose(disposing);
        }
        protected override void Update(GameTime gameTime)
        {
            OnUpdating();
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            var translationMatrix = OnSettingTranslationMatrix();
            Batch.Begin(transformMatrix: translationMatrix, blendState: BlendState.Additive);
            GraphicsDevice.Clear(Color.Black);
            OnRendering();
            Batch.End();
        }
    }
}