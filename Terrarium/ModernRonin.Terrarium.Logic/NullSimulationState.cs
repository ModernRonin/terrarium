using System.Collections.Generic;
using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic
{
    public struct NullSimulationState : ISimulationState
    {
        public Vector2D Size => new Vector2D(100, 100);
        public IEnumerable<Entity> Entities => Null.Enumerable<Entity>();
        public float[,] EnergyDensity => new float[100, 100];
        public IEnumerable<EnergySource> EnergySources => new EnergySource[0];
        public IEnumerable<Entity> GetEntitiesAt(Vector2D position) => Null.Enumerable<Entity>();
    }
}