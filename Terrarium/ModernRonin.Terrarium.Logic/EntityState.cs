using System.Collections.Generic;
using System.Linq;
using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic
{
    public class EntityState : IEntityState
    {
        public EntityState(IEnumerable<Part> parts, Vector2D position = new Vector2D())
        {
            Position = position;
            Parts = parts;
        }
        public Vector2D Position { get; }
        public IEnumerable<Part> Parts { get; }
        public string Code => string.Join("?", Parts.Select(p => p.Code));
        public Rectangle2D LocalBoundingBox
        {
            get
            {
                var minX = Parts.Min(p => p.RelativePosition.X);
                var maxX = Parts.Max(p => p.RelativePosition.X)+1;
                var minY = Parts.Min(p => p.RelativePosition.Y);
                var maxY = Parts.Max(p => p.RelativePosition.Y)+1;
                return new Rectangle2D(new Vector2D(minX, minY), new Vector2D(maxX, maxY));
            }
        }
        public Rectangle2D AbsoluteBoundingBox => LocalBoundingBox.RelativeTo(Position);
        public EntityState At(Vector2D position) => new EntityState(Parts, position);
    }
}