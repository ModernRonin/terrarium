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
        readonly SpriteBatch mSpriteBatch;
        readonly TextureDirectory mTextureDirectory;
        public Renderer(SpriteBatch spriteBatch, TextureDirectory textureDirectory)
        {
            mSpriteBatch = spriteBatch;
            mTextureDirectory = textureDirectory;
        }
        public void Render(ISimulationState simulationState)
        {
            DrawBackground(simulationState.Size);
            DrawEntities(simulationState.Entities);
        }
        void DrawEntities(IEnumerable<Entity> entities)
        {
            entities.ForEach(Draw);
        }
        void DrawBackground(Vector2D size)
        {
            mSpriteBatch.Draw(mTextureDirectory.GrayPixel,
                new Rectangle(0, 0, (int) size.X, (int) size.Y),
                Color.DarkGray);
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
}