using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ModernRonin.Terrarium.Logic.Objects.Entities;
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
            var frozen = entities as IEntityState[] ?? entities.ToArray();
            frozen.ForEach(Draw);
            mFactory.CleanAllExcept(frozen.Select(e => e.Code));
        }
        void Draw(IEntityState entityState)
        {
            var texture = mFactory.GetTextureForEntity(entityState);

            Batch.Draw(texture, entityState.AbsoluteBoundingBox.ToRectangle(), Color.White);
        }
    }
}