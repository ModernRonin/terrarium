using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModernRonin.Terrarium.Logic;
using ModernRonin.Terrarium.Rendering.Windows.Utilities;
using MoreLinq;

namespace ModernRonin.Terrarium.Rendering.Windows.Drawing
{
    public class EntityRenderer : ARenderer
    {
        readonly IEntitySpriteFactory mFactory;
        public EntityRenderer(GraphicsDevice device, SpriteBatch batch, IEntitySpriteFactory factory) : base(device,
            batch) => mFactory = factory;
        public void Render(IEnumerable<IEntityState> entities)
        {
            entities.ForEach(Draw);
        }
        void Draw(IEntityState entityState)
        {
            var texture = mFactory.GetTextureForEntity(entityState);

            Batch.Draw(texture, entityState.AbsoluteBoundingBox.ToRectangle(), Color.White);
        }
    }
}