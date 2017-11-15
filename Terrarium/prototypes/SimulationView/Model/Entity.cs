using System.Collections.Generic;
using System.Windows;

namespace SimulationView.Model
{
    public class Entity
    {
        public Vector Position { get; set; }
        public IList<Part> Parts { get; set; }
        public static Entity Snake => new Entity
        {
            Parts = new List<Part>
            {
                new Part {Kind = PartKind.Absorber, RelativePosition = new Vector(-1, 0)},
                new Part {Kind = PartKind.Absorber, RelativePosition = new Vector(-2, 0)},
                new Part {Kind = PartKind.Store, RelativePosition = new Vector(1, 0)},
                new Part {Kind = PartKind.Store, RelativePosition = new Vector(2, 0)},
                new Part {Kind = PartKind.Core, RelativePosition = new Vector()}
            }
        };
        public static Entity Cross => new Entity
        {
            Parts = new List<Part>
            {
                new Part {Kind = PartKind.Core, RelativePosition = new Vector()},
                new Part {Kind = PartKind.Absorber, RelativePosition = new Vector(-1, 0)},
                new Part {Kind = PartKind.Absorber, RelativePosition = new Vector(1, 0)},
                new Part {Kind = PartKind.Store, RelativePosition = new Vector(0, -1)},
                new Part {Kind = PartKind.Store, RelativePosition = new Vector(0, 1)}
            }
        };
    }
}