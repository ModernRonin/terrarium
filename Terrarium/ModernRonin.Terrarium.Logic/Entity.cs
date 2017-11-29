using System.Collections.Generic;
using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic
{
    public class Entity
    {
        public Vector2D Position { get; set; }
        public IList<Part> Parts { get; set; }
        public static Entity Snake => new Entity
        {
            Parts = new List<Part>
            {
                new Part(PartKind.Absorber, new Vector2D(-1, 0)),
                new Part(PartKind.Absorber, new Vector2D(-2, 0)),
                new Part(PartKind.Store, new Vector2D(1, 0)),
                new Part(PartKind.Store, new Vector2D(2, 0)),
                new Part(PartKind.Core, new Vector2D())
            }
        };
        public static Entity Cross => new Entity
        {
            Parts = new List<Part>
            {
                new Part(PartKind.Core, new Vector2D()),
                new Part(PartKind.Absorber, new Vector2D(-1, 0)),
                new Part(PartKind.Absorber, new Vector2D(1, 0)),
                new Part(PartKind.Store, new Vector2D(0, -1)),
                new Part(PartKind.Store, new Vector2D(0, 1))
            }
        };
    }
}