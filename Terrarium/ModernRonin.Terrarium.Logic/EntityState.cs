using System.Collections.Generic;
using System.Linq;
using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic
{
    public class EntityState
    {
        public EntityState(IEnumerable<Part> parts, Vector2D position = new Vector2D())
        {
            Position = position;
            Parts = parts;
        }
        public Vector2D Position { get; }
        public IEnumerable<Part> Parts { get; }
        public string Code => string.Join("?", Parts.Select(p => p.Code));
        public static EntityState Snake => new EntityState(new List<Part>
        {
            new Part(PartKind.Absorber, new Vector2D(-1, 0)),
            new Part(PartKind.Absorber, new Vector2D(-2, 0)),
            new Part(PartKind.Store, new Vector2D(1, 0)),
            new Part(PartKind.Store, new Vector2D(2, 0)),
            new Part(PartKind.Core, new Vector2D())
        });
        public static EntityState Cross => new EntityState(new List<Part>
        {
            new Part(PartKind.Core, new Vector2D()),
            new Part(PartKind.Absorber, new Vector2D(-1, 0)),
            new Part(PartKind.Absorber, new Vector2D(1, 0)),
            new Part(PartKind.Store, new Vector2D(0, -1)),
            new Part(PartKind.Store, new Vector2D(0, 1))
        });
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