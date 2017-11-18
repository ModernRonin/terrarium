using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModernRonin.Terrarium.Logic;
using MoreLinq;

namespace MonoGameUwpXaml
{
    public class VisualSimulation : Game
    {
        readonly GraphicsDeviceManager mGraphics;
        readonly Dictionary<PartKind, Texture2D> mPartKindTextures = new Dictionary<PartKind, Texture2D>();
        SpriteBatch mSpriteBatch;
        public VisualSimulation()
        {
            mGraphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        public SimulationState SimulationState { get; set; }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }
        protected override void LoadContent()
        {
            mSpriteBatch = new SpriteBatch(GraphicsDevice);

            Enum.GetNames(typeof(PartKind)).ToDictionary(Enum.Parse<PartKind>, Content.Load<Texture2D>)
                .ForEach(kvp => mPartKindTextures.Add(kvp.Key, kvp.Value));
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
            // TODO: Add your update logic here

            base.Update(gameTime);
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}