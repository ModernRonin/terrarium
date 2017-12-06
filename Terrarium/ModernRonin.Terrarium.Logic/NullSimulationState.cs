using System.Collections.Generic;
using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic
{
    public struct NullSimulationState : ISimulationState
    {
        public Vector2D Size => new Vector2D(100, 100);
        public IEnumerable<EntityState> Entities => new EntityState[0];
        public float[,] EnergyDensity => new float[100, 100];
        public IEnumerable<EnergySource> EnergySources => new EnergySource[0];
        public IEnumerable<EntityState> GetEntitiesAt(Vector2D position) => Null.Enumerable<EntityState>();
    }
}