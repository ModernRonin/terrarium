using System.Collections.Generic;
using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic
{
    public struct SimulationState : ISimulationState
    {
        public SimulationState(IEnumerable<Entity> entities, Vector2D size = new Vector2D())
        {
            Size = size;
            Entities = entities;
        }
        public static SimulationState Default => new SimulationState(
            new List<Entity> {Entity.Cross.At(new Vector2D(10, 10)), Entity.Snake.At(new Vector2D(90, 90))},
            new Vector2D(100, 100));
        public Vector2D Size { get; }
        public IEnumerable<Entity> Entities { get; }
    }
}