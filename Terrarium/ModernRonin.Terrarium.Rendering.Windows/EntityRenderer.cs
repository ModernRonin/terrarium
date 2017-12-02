using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModernRonin.Terrarium.Logic;
using MoreLinq;

namespace ModernRonin.Terrarium.Rendering.Windows
{
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
}