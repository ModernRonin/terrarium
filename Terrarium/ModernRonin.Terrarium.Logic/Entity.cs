using System.Collections.Generic;
using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic
{
    public struct Entity
    {
        public Entity(IEnumerable<Part> parts, Vector2D position = new Vector2D())
        {
            Position = position;
            Parts = parts;
        }
        public Vector2D Position { get; }
        public IEnumerable<Part> Parts { get; }
        public static Entity Snake => new Entity(new List<Part>
        {
            new Part(PartKind.Absorber, new Vector2D(-1, 0)),
            new Part(PartKind.Absorber, new Vector2D(-2, 0)),
            new Part(PartKind.Store, new Vector2D(1, 0)),
            new Part(PartKind.Store, new Vector2D(2, 0)),
            new Part(PartKind.Core, new Vector2D())
        });
        public static Entity Cross => new Entity(new List<Part>
        {
            new Part(PartKind.Core, new Vector2D()),
            new Part(PartKind.Absorber, new Vector2D(-1, 0)),
            new Part(PartKind.Absorber, new Vector2D(1, 0)),
            new Part(PartKind.Store, new Vector2D(0, -1)),
            new Part(PartKind.Store, new Vector2D(0, 1))
        });
    }
}