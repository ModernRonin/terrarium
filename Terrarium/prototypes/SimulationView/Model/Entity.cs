using System.Collections.Generic;

namespace SimulationView.Model
{
    public class Entity
    {
        public Vector2D Position { get; set; }
        public IList<Part> Parts { get; set; }
        public static Entity Snake => new Entity
        {
            Parts = new List<Part>
            {
                new Part {Kind = PartKind.Absorber, RelativePosition = new Vector2D(-1, 0)},
                new Part {Kind = PartKind.Absorber, RelativePosition = new Vector2D(-2, 0)},
                new Part {Kind = PartKind.Store, RelativePosition = new Vector2D(1, 0)},
                new Part {Kind = PartKind.Store, RelativePosition = new Vector2D(2, 0)},
                new Part {Kind = PartKind.Core, RelativePosition = Vector2D.Zero}
            }
        };
        public static Entity Cross => new Entity
        {
            Parts = new List<Part>
            {
                new Part {Kind = PartKind.Core, RelativePosition = Vector2D.Zero},
                new Part {Kind = PartKind.Absorber, RelativePosition = new Vector2D(-1, 0)},
                new Part {Kind = PartKind.Absorber, RelativePosition = new Vector2D(1, 0)},
                new Part {Kind = PartKind.Store, RelativePosition = new Vector2D(0, -1)},
                new Part {Kind = PartKind.Store, RelativePosition = new Vector2D(0, 1)}
            }
        };
    }
}