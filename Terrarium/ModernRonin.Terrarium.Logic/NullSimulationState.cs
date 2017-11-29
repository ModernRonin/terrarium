using System.Collections.Generic;
using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic
{
    public struct NullSimulationState : ISimulationState
    {
        public Vector2D Size => new Vector2D(100, 100);
        public IEnumerable<Entity> Entities => new Entity[0];
    }
}