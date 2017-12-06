using System.Collections.Generic;
using ModernRonin.Standard;

namespace ModernRonin.Terrarium.Logic
{
    public struct NullSimulationState : ISimulationState
    {
        public Vector2D Size => new Vector2D(100, 100);
        public IEnumerable<Entity> Entities => Null.Enumerable<Entity>();
        public float[,] EnergyDensity => new float[100, 100];
        public IEnumerable<IEnergySource> EnergySources => Null.Enumerable<IEnergySource>();
        public IEnumerable<Entity> GetEntitiesAt(Vector2D position) => Null.Enumerable<Entity>();
        public ISimulationState WithEnergySources(IEnumerable<IEnergySource> energySources) => this;
        public ISimulationState WithEntities(IEnumerable<Entity> entities) => this;
    }
}