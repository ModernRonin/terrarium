using System.Collections.Generic;
using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic
{
    public class SimulationState
    {
        public Vector2D Size { get; set; }
        public IEnumerable<Entity> Entities { get; set; } = new Entity[0];
        public static SimulationState Default => new SimulationState
        {
            Size = new Vector2D(100, 100),
            Entities = new List<Entity> {Entity.Cross.At(new Vector2D(10, 10)), Entity.Snake.At(new Vector2D(90, 90))}
        };
    }
}